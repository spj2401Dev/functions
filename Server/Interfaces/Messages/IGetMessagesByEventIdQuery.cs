using Functions.Shared.DTOs.Messages;

namespace Functions.Server.Interfaces.Messages
{
    public interface IGetMessagesByEventIdQuery
    {
        Task<List<MessageDTO>> GetMessagesByEventId(Guid Id);
    }
}
