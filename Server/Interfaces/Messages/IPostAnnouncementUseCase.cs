using Functions.Shared.DTOs.Messages;

namespace Functions.Server.Interfaces.Messages
{
    public interface IPostAnnouncementUseCase
    {
        Task Handle(AnnouncementRequestDTO request, Guid userId);
    }
}
