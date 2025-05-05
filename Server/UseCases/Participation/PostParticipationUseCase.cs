using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Participation;
using Functions.Server.Model;
using Functions.Shared.Enum;

namespace Functions.Server.UseCases.Participation
{
    public class PostParticipationUseCase(IEventVisitorQuery eventVisitorQuery,
                                          IRepository<EventVisitor> eventVisitorRepository) : IPostParticipationUseCase
    {
        public async Task Handle(Guid userId, Guid eventId, ParticipationStatus type)
        {
            EventVisitor? eventVisitorEntity = await eventVisitorQuery.GetVisitorByEventAndUserId(userId, eventId);

            var newEventVisitor = new EventVisitor
            {
                UserId = userId,
                EventId = eventId,
                Type = type,
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
        }
    }
}
