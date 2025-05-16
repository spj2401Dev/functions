using Functions.Shared.DTOs.Event;

namespace Functions.Server.Interfaces.Event
{
    public interface IUpdateEventUseCase
    {
        Task Handle(EventsDTO request, Guid userId);
    }
}
