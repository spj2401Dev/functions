using Functions.Shared.DTOs.Messages;

namespace Functions.Server.Interfaces.Messages
{
    public interface IPostCommentUseCase
    {
        Task Handle(CommentRequestDTO request, Guid userId);
    }
}
