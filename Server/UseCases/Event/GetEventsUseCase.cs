using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Event;
using Functions.Server.Model;
using Functions.Shared.DTOs;

namespace Functions.Server.UseCases.Event
{
    public class GetEventsUseCase(IRepository<Events> eventRepository) : IGetEventsUseCase
    {
        public async Task<List<EventsDTO>> Handle()
        {
            var events = await eventRepository.GetAllAsync();
            events = events.Where(e => e.IsPublic == true)
                           .Where(e => e.StartDateTime >= DateTime.Now)
                           .OrderBy(e => e.StartDateTime).ToList();

            var result = events.Select(e => new EventsDTO(
                Id: e.Id,
                HostId: e.Host,
                Name: e.Name,
                Location: e.Location,
                Description: e.Description ?? string.Empty,
                StartDateTime: e.StartDateTime,
                EndDateTime: e.EndDateTime,
                isPublic: e.IsPublic,
                ProfilePictureBase64: null,
                FileId: e.PictureId
            )).ToList();

            return result;
        }
    }
}
