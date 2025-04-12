using Functions.Server.Interfaces.Participation;
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
                                         IPostParticipationUseCase postParticipationUseCase) : FunctionsControllerBase(configuration), IParticipationProxy
    {
        [HttpPost("PostParticipation")]
        public async Task PostParticipation([FromBody] PostParticipationDTO request)
        {
            var userId = await GetUserIdFromTokenAsync() ?? throw new UnauthorizedAccessException();
            await postParticipationUseCase.Handle(userId, request.EventId, request.Type);
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
                    Status = ev.Type,
                }).ToList(),
                Status = requestorStatus
            };
        }
    }
}
