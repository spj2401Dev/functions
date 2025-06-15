using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Event;
using Functions.Server.Model;
using Functions.Shared.DTOs.Event;
using Functions.Server.Services;

namespace Functions.Server.UseCases.Event
{
    public class GetEventsUseCase(
        IRepository<Events> eventRepository,
        LuceneEventSearchService luceneService) : IGetEventsUseCase
    {
        public async Task<IEnumerable<Events>> ExecuteAsync()
        {
            var events = await eventRepository.GetAllAsync();
            return events;
        }

        public async Task<List<EventMasterPageDTO>> Handle()
        {
            var events = await eventRepository.GetAllAsync();
            events = events.Where(e => e.IsPublic == true)
                            .Where(e => e.StartDateTime >= DateTime.Now)
                            .OrderBy(e => e.StartDateTime)
                            .ToList();

            var result = events.Select(e => new EventMasterPageDTO
            {
                Id = e.Id,
                Name = e.Name,
                ImageId = e.PictureId,
                StartDate = e.StartDateTime,
                EndDate = e.EndDateTime,
                Location = e.Location,
                Description = e.Description
            }).ToList();

            return result;
        }
    }
}
