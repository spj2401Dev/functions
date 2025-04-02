using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Event;
using Functions.Server.Model;
using Functions.Shared.DTOs;

namespace Functions.Server.UseCases.Event
{
    public class GetEventByIdUseCase(IRepository<Events> eventRepository, IRepository<Files> fileRepository) : IGetEventByIdUseCase
    {
        public async Task<EventsDTO> Handle(Guid id)
        {
            var @event = await eventRepository.GetByIdAsync(id);
            if (@event == null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            string? base64Image = null;
            if (@event.PictureId.HasValue)
            {
                var file = await fileRepository.GetByIdAsync(@event.PictureId.Value);
                if (file != null && file.FileContent != null)
                {
                    base64Image = file.FileContent.Base64Content;
                }
            }

            return new EventsDTO(
                @event.Id,
                @event.Host,
                @event.Name,
                @event.Location,
                @event.Description ?? string.Empty,
                @event.StartDateTime,
                @event.EndDateTime,
                base64Image,
                null,
                null,
                @event.IsPublic);
        }
    }
}
