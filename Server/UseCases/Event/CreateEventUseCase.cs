using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Event;
using Functions.Server.Model;
using Functions.Server.Services.File;
using Functions.Server.Services;
using Functions.Shared.DTOs.Event;

namespace Functions.Server.UseCases.Event
{
    public class CreateEventUseCase : ICreateEventUseCase
    {
        private readonly IRepository<Events> _eventRepository;
        private readonly LuceneEventSearchService _luceneService;
        private readonly FilesService _filesService;

        public CreateEventUseCase(
            IRepository<Events> eventRepository,
            LuceneEventSearchService luceneService,
            FilesService filesService)
        {
            _eventRepository = eventRepository;
            _luceneService = luceneService;
            _filesService = filesService;
        }

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
                var fileId = await _filesService.SaveFileAsync(request.ProfilePictureBase64, request.FileName, request.FileType);
                
                newEvent.PictureId = fileId;
            }

            await _eventRepository.AddAsync(newEvent);
            _luceneService.IndexEvent(newEvent);
        }
    }
}
