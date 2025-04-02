using Functions.Shared.DTOs;

namespace Functions.Server.Interfaces.Event
{
    public interface IGetEventByIdUseCase
    {
        Task<EventsDTO> Handle(Guid id);
    }
}
