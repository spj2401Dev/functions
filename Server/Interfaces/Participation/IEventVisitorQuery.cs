using Functions.Server.Model;
using Functions.Shared.Enum;

namespace Functions.Server.Interfaces.Participation
{
    public interface IEventVisitorQuery
    {
        Task<IEnumerable<EventVisitor>> GetAllByEventId(Guid eventId);
        Task<List<EventVisitor>> GetAllEventsByUserId(Guid userId);
        Task<EventVisitor?> GetVisitorByEventAndUserId(Guid userId, Guid EventId);
        Task<ParticipationStatus?> GetUserStatusByEventId(Guid UserId, Guid EventId);
    }
}
