using Functions.Server.Interfaces;
using Functions.Server.Model;
using Functions.Server.Services;
using Functions.Shared.DTOs.Event;
using Functions.Shared.Interfaces.Search;
using Microsoft.AspNetCore.Mvc;

namespace Functions.Server.Controller
{
    [Controller]
    [Route("api/[controller]")]
    public class SearchController(
        IConfiguration configuration,
        IRepository<Events> eventRepository,
        LuceneEventSearchService luceneService) : FunctionsControllerBase(configuration), ISearchProxy
    {
        [HttpGet("search")]
        public async Task<IEnumerable<EventMasterPageDTO>> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentException();

            var allEvents = (await eventRepository.GetAllAsync()).ToList();

            var results = luceneService.Search(query, allEvents).Where(x => x.EndDate > DateTime.Now); // Nicht anderst möglich wegen Search Index.

            return results;
        }
    }
}
