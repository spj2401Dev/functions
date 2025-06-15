using Functions.Server.Interfaces;
using Functions.Server.Model;
using Functions.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace Functions.Server.Repsitorys
{
    public class EventsRepository(FunctionsDbContext context, LuceneEventSearchService luceneService) : IRepository<Events>
    {
        public async Task<IEnumerable<Events>> GetAllAsync()
        {
            return await context.Events.ToListAsync();
        }

        public async Task<Events> GetByIdAsync(Guid id)
        {
            return await context.Events.AsNoTracking().FirstAsync(x => x.Id == id);
        }

        public async Task AddAsync(Events entity)
        {
            await context.Events.AddAsync(entity);
            await context.SaveChangesAsync();
            luceneService.IndexEvent(entity);
        }

        public async Task UpdateAsync(Events entity)
        {
            context.Events.Update(entity);
            await context.SaveChangesAsync();
            luceneService.IndexEvent(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await context.Events.FindAsync(id);
            if (entity != null)
            {
                context.Events.Remove(entity);
                await context.SaveChangesAsync();
                luceneService.DeleteEvent(id);
            }
        }
    }
}
