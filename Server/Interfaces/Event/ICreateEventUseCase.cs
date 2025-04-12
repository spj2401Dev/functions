using Functions.Shared.DTOs.Event;

namespace Functions.Server.Interfaces.Event
{
    public interface ICreateEventUseCase
    {
        Task Handle(EventsDTO request, Guid userId);
    }
}
