using Functions.Server.Interfaces;
using Functions.Server.Model;
using Functions.Server.Services;

public class EventInitializationService(IRepository<Events> eventRepository, LuceneEventSearchService luceneService)
{
    public async Task InitializeEventsAsync()
    {
        var allEvents = await eventRepository.GetAllAsync();
        luceneService.RebuildIndex(allEvents);
    }
}
