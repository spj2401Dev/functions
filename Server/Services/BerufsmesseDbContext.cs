using Functions.Server.Interfaces;
using Functions.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace Functions.Server.Services
{
    public class BerufsmesseDbContext : DbContext, IBerufsmesseDbContext
    {
        protected string Schema = "dbo";

        public DbSet<Person> Persons { get; set; }

        public BerufsmesseDbContext(DbContextOptions<BerufsmesseDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (!string.IsNullOrWhiteSpace(Schema))
            {
                modelBuilder.HasDefaultSchema(Schema);
            }
            base.OnModelCreating(modelBuilder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(true, cancellationToken);
        }
    }
}
