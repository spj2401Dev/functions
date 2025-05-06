using Functions.Server.Interfaces.Messages;
using Functions.Server.Services;
using Functions.Shared.DTOs.Messages;
using Functions.Shared.Interfaces.Messages;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Functions.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController(IConfiguration configuration,
                                    IPostAnnouncementUseCase postAnnouncementUseCase,
                                    IGetMessagesByEventIdQuery getMessageQuery,
                                    IPostCommentUseCase postCommentUseCase) : FunctionsControllerBase(configuration), IMessageProxy
    {
        [HttpPost("PostAnnouncement")]
        public async Task<HttpResponseMessage> PostAnnouncement([FromBody] AnnouncementRequestDTO request)
        {
            var userId = await GetUserIdFromTokenAsync();

            if (userId == null)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            await postAnnouncementUseCase.Handle(request, userId.Value);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [HttpPost("PostMessage")]
        public async Task<HttpResponseMessage> PostMessage([FromBody] CommentRequestDTO request)
        {
            var userId = await GetUserIdFromTokenAsync() ?? throw new UnauthorizedAccessException();

            await postCommentUseCase.Handle(request, userId);

            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        [HttpGet("GetMessagesForEvent")]
        public async Task<List<MessageDTO>> GetMessagesForEvent([FromQuery] Guid eventId)
        {
            return await getMessageQuery.GetMessagesByEventId(eventId);
        }
    }
}
