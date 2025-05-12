using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Event;
using Functions.Server.Model;
using Functions.Server.Services.File;
using Functions.Shared.DTOs.Event;

namespace Functions.Server.UseCases.Event
{
    public class UpdateEventUseCase(IRepository<Events> eventRepository, FilesService filesService) : IUpdateEventUseCase
    {
        public async Task Handle(EventsDTO request, Guid userId)
        {
            var oldEvent = await eventRepository.GetByIdAsync(request.Id);

            if (oldEvent.Host != userId)
            {
                throw new UnauthorizedAccessException();
            }

            var Event = new Events
            {
                Id = request.Id,
                Host = userId,
                Name = request.Name,
                Location = request.Location,
                Description = request.Description,
                StartDateTime = request.StartDateTime,
                EndDateTime = request.EndDateTime,
                IsPublic = request.isPublic,
            };

            if (!string.IsNullOrEmpty(request.ProfilePictureBase64) &&
                !string.IsNullOrEmpty(request.FileName) &&
                !string.IsNullOrEmpty(request.FileType))
            {
                var fileId = await filesService.SaveFileAsync(request.ProfilePictureBase64, request.FileName, request.FileType);

                Event.PictureId = fileId;
            }
            await eventRepository.UpdateAsync(Event);
        }
    }
}
