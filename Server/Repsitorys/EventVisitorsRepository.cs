using Functions.Server.Interfaces;
using Functions.Server.Model;
using Functions.Server.Services;

namespace Functions.Server.Repsitorys
{
    public class EventVisitorsRepository(FunctionsDbContext context) : IRepository<EventVisitor>
    {
        public async Task<IEnumerable<EventVisitor>> GetAllAsync()
        {
            throw new NotSupportedException();
        }

        public async Task<EventVisitor> GetByIdAsync(Guid id)
        {
            return await context.EventVisitors.FindAsync(id);
        }

        public async Task AddAsync(EventVisitor entity)
        {
            await context.EventVisitors.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EventVisitor entity)
        {
            context.EventVisitors.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
