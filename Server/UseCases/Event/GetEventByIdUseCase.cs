using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Event;
using Functions.Server.Model;
using Functions.Shared.DTOs.Event;

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

            return new EventsDTO()
            {
                Id = @event.Id,
                HostId = @event.Host,
                Name = @event.Name,
                Location = @event.Location,
                Description = @event.Description ?? string.Empty,
                StartDateTime = @event.StartDateTime,
                EndDateTime = @event.EndDateTime,
                isPublic = @event.IsPublic,
                ProfilePictureBase64 = null,
                FileName = null,
                FileType = null,
                FileId = @event.PictureId
            };
        }
    }
}
