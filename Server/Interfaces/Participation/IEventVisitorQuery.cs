using Functions.Server.Model;

namespace Functions.Server.Interfaces.Participation
{
    public interface IEventVisitorQuery
    {
        Task<List<EventVisitor>> GetAllByEventId(Guid eventId);
        Task<List<EventVisitor>> GetAllEventsByUserId(Guid userId);
        Task<EventVisitor?> GetVisitorByEventAndUserId(Guid userId, Guid EventId);
    }
}
