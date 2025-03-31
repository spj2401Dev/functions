using Functions.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace Functions.Server.Interfaces
{
    public interface IBerufsmesseDbContext
    {
        public DbSet<Person> Persons { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
