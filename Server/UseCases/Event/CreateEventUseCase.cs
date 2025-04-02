using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Event;
using Functions.Server.Model;
using Functions.Shared.DTOs;

namespace Functions.Server.UseCases.Event
{
    public class CreateEventUseCase(IRepository<Events> eventRepository, IRepository<Files> fileRepository) : ICreateEventUseCase
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
                EndDateTime = request.EndDateTime
            };

            if (!string.IsNullOrEmpty(request.ProfilePictureBase64) &&
                !string.IsNullOrEmpty(request.FileName) &&
                !string.IsNullOrEmpty(request.FileType))
            {
                try
                {
                    var fileContent = new FileContent
                    {
                        Id = Guid.NewGuid(), // think about it
                        Base64Content = request.ProfilePictureBase64
                    };

                    var fileRecord = new Files
                    {
                        Id = Guid.NewGuid(),
                        FileName = request.FileName,
                        FileType = request.FileType,
                        FileContentId = fileContent.Id,
                        FileContent = fileContent
                    };
                    newEvent.PictureId = fileRecord.Id;

                    await fileRepository.AddAsync(fileRecord);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving file: {ex.Message}"); // logger implementation?
                    newEvent.PictureId = null;
                }
            }

            await eventRepository.AddAsync(newEvent);
        }
    }
}
