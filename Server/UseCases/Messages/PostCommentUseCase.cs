using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Messages;
using Functions.Server.Model;
using Functions.Shared.DTOs.Messages;

namespace Functions.Server.UseCases.Messages
{
    public class PostCommentUseCase(IRepository<Message> messageRepository) : IPostCommentUseCase
    {
        public async Task Handle(CommentRequestDTO request, Guid userId)
        {
            if (request.EventId == null)
            {
                throw new ArgumentNullException(nameof(request.EventId), "EventId cannot be null");
            }

            var newMessage = new Message()
            {
                MessageDate = DateTime.UtcNow,
                CreatorId = userId,
                Text = request.Comment, 
                EventId = request.EventId ?? Guid.Empty,
                ParentId = request.ParentId ?? null,
                Type = Shared.Enum.MessageTypes.Comment
            };

            await messageRepository.AddAsync(newMessage);
        }
    }
}
