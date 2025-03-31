using Functions.Shared.DTOs;

namespace Functions.Shared.Interfaces
{
    public interface IEventsProxy
    {
        Task<List<EventsDTO>> GetEventsAsync();
        Task<HttpResponseMessage> PostEventAsync(EventsDTO request);
    }
}
