using Functions.Shared.DTOs;

namespace Functions.Shared.Interfaces
{
    public interface IEventsProxy
    {
        Task<List<EventsDTO>> GetEventsAsync();

        Task<EventsDTO?> GetEventsbyIdAsync(Guid Id);
        Task<HttpResponseMessage> PostEventAsync(EventsDTO request);

        Task<List<EventsDTO>> GetPublicEventsAsync();
    }
}
