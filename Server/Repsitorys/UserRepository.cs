using Functions.Server.Interfaces;
using Functions.Server.Model;
using Functions.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace Functions.Server.Repsitorys
{
    public class UserRepository(FunctionsDbContext context) : IRepository<User>
    {
        public async Task AddAsync(User entity)
        {
            await context.Users.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(User entity)
        {
            context.Users.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
