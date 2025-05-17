using Functions.Server.Interfaces.Participation;
using Functions.Server.Services;
using Functions.Shared.DTOs.Participation;
using Functions.Shared.Enum;
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
            Guid? userId = await GetUserIdFromTokenAsync();
            var eventVisitors = await eventVisitorQuery.GetAllByEventId(EventId);

            ParticipationStatus? requestorStatus = null;

            if (userId != null && userId != Guid.Empty)
            {
                requestorStatus = await eventVisitorQuery.GetUserStatusByEventId(userId ?? throw new UnauthorizedAccessException(), EventId);
            }

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
