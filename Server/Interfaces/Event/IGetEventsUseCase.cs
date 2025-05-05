using Functions.Shared.DTOs.Event;

namespace Functions.Server.Interfaces.Event
{
    public interface IGetEventsUseCase
    {
        Task<List<EventMasterPageDTO>> Handle();
    }
}
