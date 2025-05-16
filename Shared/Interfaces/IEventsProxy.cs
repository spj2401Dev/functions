using Functions.Shared.DTOs.Event;

namespace Functions.Shared.Interfaces
{
    public interface IEventsProxy
    {
        Task<List<EventMasterPageDTO>> GetEventsAsync();
        Task<EventsDTO?> GetEventById(Guid Id);
        Task<HttpResponseMessage> PostEventAsync(EventsDTO request);
        Task<HttpResponseMessage> PutEventAsync(EventsDTO request);
        Task<HomePageResponseDTO> GetAllEventsByUserAsync();
    }
}
