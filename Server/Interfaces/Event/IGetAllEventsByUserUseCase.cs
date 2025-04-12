using Functions.Shared.DTOs.Event;

namespace Functions.Server.Interfaces.Event
{
    public interface IGetAllEventsByUserUseCase
    {
        Task<List<EventMasterPageDTO>> Handle(Guid userId);
    }
}
