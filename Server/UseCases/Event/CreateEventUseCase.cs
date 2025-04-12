using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Event;
using Functions.Server.Model;
using Functions.Server.Services.File;
using Functions.Shared.DTOs.Event;

namespace Functions.Server.UseCases.Event
{
    public class CreateEventUseCase(IRepository<Events> eventRepository, FilesService filesService) : ICreateEventUseCase
    {
        public async Task Handle(EventsDTO request, Guid userId)
        {
            var newEvent = new Events
            {
                Id = Guid.NewGuid(),
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
                
                newEvent.PictureId = fileId;
            }

            await eventRepository.AddAsync(newEvent);
        }
    }
}
