using Functions.Shared.DTOs;

namespace Functions.Server.Interfaces.Event
{
    public interface IGetPublicEventsUseCase
    {
        Task<List<EventsDTO>> Handle();
    }
}
