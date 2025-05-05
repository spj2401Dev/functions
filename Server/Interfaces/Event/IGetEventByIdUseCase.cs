using Functions.Shared.DTOs.Event;

namespace Functions.Server.Interfaces.Event
{
    public interface IGetEventByIdUseCase
    {
        Task<EventsDTO> Handle(Guid id);
    }
}
