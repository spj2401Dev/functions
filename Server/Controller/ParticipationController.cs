using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Participation;
using Functions.Server.Model;
using Functions.Server.Services;
using Functions.Shared.DTOs.Participation;
using Microsoft.AspNetCore.Mvc;

namespace Functions.Server.Controller
{
    [Controller]
    [Route("api/[controller]")]
    public class ParticipationController(IConfiguration configuration,
                                         IEventVisitorQuery eventVisitorQuery,
                                         IRepository<EventVisitor> eventVisitorRepository) : FunctionsControllerBase(configuration)
    {
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
    }
}
