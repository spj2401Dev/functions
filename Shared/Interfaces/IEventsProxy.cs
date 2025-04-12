using Functions.Shared.DTOs.Event;

namespace Functions.Shared.Interfaces
{
    public interface IEventsProxy
    {
        Task<List<EventMasterPageDTO>> GetEventsAsync();

        Task<EventsDTO?> GetEventsbyIdAsync(Guid Id);
        Task<HttpResponseMessage> PostEventAsync(EventsDTO request);

        Task<List<EventsDTO>> GetFilteredEventsAsync(bool isPublic);
        Task<List<EventMasterPageDTO>> GetAllEventsByUserAsync();
    }
}
