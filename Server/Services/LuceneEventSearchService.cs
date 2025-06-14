using Functions.Server.Model;
using Functions.Shared.DTOs.Event;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Lucene.Net.QueryParsers.Classic;

namespace Functions.Server.Services
{
    public class LuceneEventSearchService : IDisposable
    {
        private static readonly LuceneVersion AppLuceneVersion = LuceneVersion.LUCENE_48;

        private readonly string indexPath;
        private readonly FSDirectory directory;
        private readonly StandardAnalyzer analyzer;
        private readonly IndexWriter writer;
        private readonly object writerLock;

        public LuceneEventSearchService(IConfiguration configuration)
        {
            indexPath = configuration["Lucene:IndexPath"] ?? "lucene_index";
            directory = FSDirectory.Open(indexPath);
            analyzer = new StandardAnalyzer(AppLuceneVersion);
            var indexConfig = new IndexWriterConfig(AppLuceneVersion, analyzer);
            writer = new IndexWriter(directory, indexConfig);
            writerLock = new object();
        }

        public void IndexEvent(Events ev)
        {
            var document = new Document
            {
                new StringField("Id", ev.Id.ToString(), Field.Store.YES),
                new TextField("Name", ev.Name ?? string.Empty, Field.Store.YES),
                new TextField("Description", ev.Description ?? string.Empty, Field.Store.YES),
                new TextField("Location", ev.Location ?? string.Empty, Field.Store.YES)
            };

            lock (writerLock)
            {
                writer.UpdateDocument(new Term("Id", ev.Id.ToString()), document);
                writer.Flush(triggerMerge: false, applyAllDeletes: false);
            }
        }

        public void DeleteEvent(Guid eventId)
        {
            lock (writerLock)
            {
                writer.DeleteDocuments(new Term("Id", eventId.ToString()));
                writer.Flush(triggerMerge: false, applyAllDeletes: false);
            }
        }

        public List<EventMasterPageDTO> Search(string query, IEnumerable<Events> allEvents)
        {
            var results = new List<EventMasterPageDTO>();

            try
            {
                lock (writerLock)
                {
                    using var reader = writer.GetReader(applyAllDeletes: true);
                    var searcher = new IndexSearcher(reader);

                    var parser = new MultiFieldQueryParser(
                        AppLuceneVersion,
                        new[] { "Name", "Description", "Location" },
                        analyzer
                    );

                    Query luceneQuery;
                    try
                    {
                        luceneQuery = parser.Parse(query);
                    }
                    catch (ParseException)
                    {
                        luceneQuery = new TermQuery(new Term("Name", query.ToLowerInvariant()));
                    }

                    var hits = searcher.Search(luceneQuery, 20).ScoreDocs;

                    foreach (var hit in hits)
                    {
                        var document = searcher.Doc(hit.Doc);
                        if (Guid.TryParse(document.Get("Id"), out var id))
                        {
                            var ev = allEvents.FirstOrDefault(e => e.Id == id);
                            if (ev != null)
                            {
                                results.Add(new EventMasterPageDTO
                                {
                                    Id = ev.Id,
                                    Name = ev.Name ?? string.Empty,
                                    ImageId = ev.PictureId,
                                    StartDate = ev.StartDateTime,
                                    EndDate = ev.EndDateTime,
                                    Location = ev.Location ?? string.Empty,
                                    Description = ev.Description
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Däd I mir an Kopf mache
            }

            return results;
        }

        public void RebuildIndex(IEnumerable<Events> allEvents)
        {
            lock (writerLock)
            {
                writer.DeleteAll();
                foreach (var ev in allEvents)
                {
                    IndexEvent(ev);
                }
                writer.Flush(triggerMerge: false, applyAllDeletes: false);
            }
        }

        public void Dispose()
        {
            writer?.Dispose();
            analyzer?.Dispose();
            directory?.Dispose();
        }
    }
}
