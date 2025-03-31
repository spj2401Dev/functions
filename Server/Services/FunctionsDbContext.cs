using Functions.Server.Interfaces;
using Functions.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace Functions.Server.Services
{
    public class FunctionsDbContext : DbContext, IFunctionsDbContext
    {
        protected string Schema = "dbo";

        public DbSet<Events> Events { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<FileContent> FileContent { get; set; }

        public FunctionsDbContext(DbContextOptions<FunctionsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (!string.IsNullOrWhiteSpace(Schema))
            {
                modelBuilder.HasDefaultSchema(Schema);
            }

            modelBuilder.Entity<Files>()
                .HasOne<FileContent>()
                .WithMany()
                .HasForeignKey(f => f.FileContentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Events>()
                .HasOne<Files>()
                .WithMany()
                .HasForeignKey(e => e.PictureId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(true, cancellationToken);
        }
    }
}
