using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Participation;
using Functions.Server.Model;
using Functions.Server.Services;
using Functions.Shared.DTOs.Participation;
using Functions.Shared.Interfaces.Participation;
using Microsoft.AspNetCore.Mvc;

namespace Functions.Server.Controller
{
    [Controller]
    [Route("api/[controller]")]
    public class ParticipationController(IConfiguration configuration,
                                         IEventVisitorQuery eventVisitorQuery,
                                         IRepository<EventVisitor> eventVisitorRepository) : FunctionsControllerBase(configuration), IParticipationProxy
    {
        [HttpPost("PostParticipation")]
        public async Task PostParticipation([FromBody] PostParticipationDTO request)
        {
            var userId = await GetUserIdFromTokenAsync();

            if (userId == null)
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            EventVisitor? eventVisitorEntity = await eventVisitorQuery.GetVisitorByEventAndUserId(userId.Value, request.EventId);

            var newEventVisitor = new EventVisitor
            {
                UserId = userId.Value,
                EventId = request.EventId,
                Type = request.Type
            };

            if (eventVisitorEntity == null)
            {
                await eventVisitorRepository.AddAsync(newEventVisitor);
            }
            else
            {
                newEventVisitor.Id = eventVisitorEntity.Id;
                await eventVisitorRepository.UpdateAsync(newEventVisitor);
            }

            Response.StatusCode = StatusCodes.Status200OK;
        }

        [HttpGet("GetParticipation")]
        public async Task<GetParticipationResponseDTO> GetParticipaton([FromQuery] Guid EventId)
        {
            var userId = await GetUserIdFromTokenAsync() ?? throw new UnauthorizedAccessException();
            var eventVisitors = await eventVisitorQuery.GetAllByEventId(EventId);
            var requestorStatus = await eventVisitorQuery.GetUserStatusByEventId(userId, EventId);

            return new GetParticipationResponseDTO()
            {
                Users = eventVisitors.Select(ev => new GetParticipationUsersResponseDTO
                {
                    UserName = ev.User.Username,
                    LastName = ev.User.Lastname,
                    FirstName = ev.User.Firstname,
                }).ToList(),
                Status = requestorStatus
            };
        }
    }
}
