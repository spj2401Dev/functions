using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Event;
using Functions.Server.Model;
using Functions.Shared.DTOs;

namespace Functions.Server.UseCases.Event
{
    public class GetFilteredEventsUseCase(IRepository<Events> eventRepository, IRepository<Files> fileRepository) : IGetFilteredEventsUseCase
    {
        public async Task<List<EventsDTO>> Handle(bool isPublic)
        {
            var events = await eventRepository.GetAllAsync();
            events = events.Where(e => e.IsPublic == isPublic).ToList();
            var result = new List<EventsDTO>();

            foreach (var e in events)
            {
                string? base64Image = null;
                if (e.PictureId.HasValue)
                {
                    var file = await fileRepository.GetByIdAsync(e.PictureId.Value);
                    if (file != null && file.FileContent != null)
                    {
                        base64Image = file.FileContent.Base64Content;
                    }
                }

                result.Add(new EventsDTO(
                    e.Id,
                    e.Host,
                    e.Name,
                    e.Location,
                    e.Description ?? string.Empty,
                    e.StartDateTime,
                    e.EndDateTime,
                    base64Image,
                    null,
                    null,
                    e.IsPublic
                ));
            }

            return result;
        }
    }
}
