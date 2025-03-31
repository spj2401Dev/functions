using Functions.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace Functions.Server.Interfaces
{
    public interface IFunctionsDbContext
    {
        public DbSet<Events> Events { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<FileContent> FileContent { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
