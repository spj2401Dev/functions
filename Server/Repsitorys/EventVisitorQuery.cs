using Functions.Server.Interfaces.Participation;
using Functions.Server.Model;
using Functions.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace Functions.Server.Repsitorys
{
    public class EventVisitorQuery(FunctionsDbContext context) : IEventVisitorQuery
    {

        public async Task<List<EventVisitor>> GetAllByEventId(Guid eventId)
        {
            return await context.EventVisitors
                .Where(ev => ev.EventId == eventId)
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

        public async Task<EventVisitor?> GetVisitorByEventAndUserId(Guid userId, Guid EventId)
        {
            return await context.EventVisitors
                .AsNoTracking()
                .FirstOrDefaultAsync(ev => ev.UserId == userId && ev.EventId == EventId);
        }
    }
}
