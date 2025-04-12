using Functions.Server.Interfaces.Participation;
using Functions.Server.Model;
using Functions.Server.Services;
using Functions.Shared.Enum;
using Microsoft.EntityFrameworkCore;

namespace Functions.Server.Repsitorys
{
    public class EventVisitorQuery(FunctionsDbContext context) : IEventVisitorQuery
    {

        public async Task<IEnumerable<EventVisitor>> GetAllByEventId(Guid eventId)
        {
            return await context.EventVisitors
                .Where(ev => ev.EventId == eventId)
                .Include(ev => ev.User)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<EventVisitor>> GetAllEventsByUserId(Guid userId)
        {
            return await context.EventVisitors
                .Where(ev => ev.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ParticipationStatus?> GetUserStatusByEventId(Guid UserId, Guid EventId)
        {
            return await context.EventVisitors
                .Where(ev => ev.UserId == UserId && ev.EventId == EventId)
                .Select(ev => ev.Type)
                .FirstOrDefaultAsync();
        }

        public async Task<EventVisitor?> GetVisitorByEventAndUserId(Guid userId, Guid EventId)
        {
            return await context.EventVisitors
                .AsNoTracking()
                .FirstOrDefaultAsync(ev => ev.UserId == userId && ev.EventId == EventId);
        }
    }
}
