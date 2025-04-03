using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Messages;
using Functions.Server.Model;
using Functions.Shared.DTOs.Messages;

namespace Functions.Server.UseCases.Messages
{
    public class PostAnnouncementUseCase(IRepository<Message> messageRepository) : IPostAnnouncementUseCase
    {
        public async Task Handle(AnnouncementRequestDTO request, Guid userId)
        {
            if (request.EventId == null)
            {
                throw new ArgumentNullException(nameof(request.EventId), "EventId cannot be null");
            }

            var newMessage = new Message()
            {
                MessageDate = DateTime.UtcNow,
                CreatorId = userId,
                Text = request.Message,
                EventId = request.EventId ?? Guid.Empty,
                Parent = null,
            };

            await messageRepository.AddAsync(newMessage);
        }
    }
}
