using Functions.Shared.DTOs;

namespace Functions.Server.Interfaces.Event
{
    public interface IGetEventsUseCase
    {
        Task<List<EventsDTO>> Handle();
    }
}
