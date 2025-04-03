using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Event;
using Functions.Server.Model;
using Functions.Shared.DTOs;

namespace Functions.Server.UseCases.Event
{
    public class GetEventByIdUseCase(IRepository<Events> eventRepository) : IGetEventByIdUseCase
    {
        public async Task<EventsDTO> Handle(Guid id)
        {
            var @event = await eventRepository.GetByIdAsync(id);
            if (@event == null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            return new EventsDTO(
                @event.Id,
                @event.Host,
                @event.Name,
                @event.Location,
                @event.Description ?? string.Empty,
                @event.StartDateTime,
                @event.EndDateTime,
                @event.IsPublic,
                null,
                null,
                null,
                @event.PictureId);
        }
    }
}
