using Functions.Shared.DTOs;

namespace Functions.Server.Interfaces.Event
{
    public interface IGetFilteredEventsUseCase
    {
        Task<List<EventsDTO>> Handle(bool isPublic);
    }
}
