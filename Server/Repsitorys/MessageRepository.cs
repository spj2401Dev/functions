using Functions.Server.Interfaces;
using Functions.Server.Model;
using Functions.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace Functions.Server.Repsitorys
{
    public class MessageRepository(FunctionsDbContext context) : IRepository<Message>
    {
        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            // Macht kein sinn. Sollte kein fall geben wo man alle, von allen Events braucht
            throw new NotSupportedException();
        }

        public async Task<Message?> GetByIdAsync(Guid id)
        {
            return await context.Messages.Include(m => m.Event).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Message entity)
        {
            await context.Messages.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Message entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

    }
}
