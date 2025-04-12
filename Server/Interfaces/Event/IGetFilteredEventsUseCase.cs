using Functions.Shared.DTOs.Event;

namespace Functions.Server.Interfaces.Event
{
    public interface IGetFilteredEventsUseCase
    {
        Task<List<EventsDTO>> Handle(bool isPublic);
    }
}
