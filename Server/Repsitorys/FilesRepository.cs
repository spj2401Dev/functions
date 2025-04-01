using Functions.Server.Interfaces;
using Functions.Server.Model;
using Functions.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace Functions.Server.Repsitorys
{
    public class FilesRepository(FunctionsDbContext context) : IRepository<Files>
    {
        public async Task<IEnumerable<Files>> GetAllAsync()
        {
            return await context.Files.Include(f => f.FileContent).ToListAsync();
        }

        public async Task<Files> GetByIdAsync(Guid Id)
        {
            return await context.Files.Include(f => f.FileContent).FirstOrDefaultAsync(f => f.Id == Id);
        }

        public async Task AddAsync(Files entity)
        {
            if (entity.FileContent != null)
            {
                if (context.Entry(entity.FileContent).State == EntityState.Detached)
                {
                    context.FileContent.Add(entity.FileContent);
                }
            }

            await context.Files.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Files entity)
        {
            context.Files.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid Id)
        {
            var entity = await context.Files.Include(f => f.FileContent).FirstOrDefaultAsync(f => f.Id == Id);
            if (entity != null)
            {
                context.Files.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
