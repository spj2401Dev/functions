using Functions.Shared.DTOs;

namespace Functions.Server.Interfaces.Event
{
    public interface ICreateEventUseCase
    {
        Task Handle(EventsDTO request, Guid userId);
    }
}
