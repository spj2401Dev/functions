using Functions.Shared.DTOs.Messages;

namespace Functions.Shared.Interfaces.Messages
{
    public interface IMessageProxy
    {
        Task<HttpResponseMessage> PostAnnouncement(AnnouncementRequestDTO request);
        Task<List<MessageDTO>> GetMessagesForEvent(Guid eventId);
    }
}
