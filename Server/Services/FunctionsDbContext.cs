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
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<EventVisitor> EventVisitors { get; set; }

        public FunctionsDbContext(DbContextOptions<FunctionsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (!string.IsNullOrWhiteSpace(Schema))
            {
                modelBuilder.HasDefaultSchema(Schema);
            }

            modelBuilder.Entity<Events>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Events__3214EC07F52E26D9");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsFixedLength();
                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .IsFixedLength();
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsFixedLength();

                entity.HasOne(d => d.Picture).WithMany(p => p.Events)
                    .HasForeignKey(d => d.PictureId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PictureId");
            });

            modelBuilder.Entity<EventVisitor>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__EventVis__3214EC0701422FC9");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Event).WithMany(p => p.EventVisitors)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventVisitors_Event");

                entity.HasOne(d => d.User).WithMany(p => p.EventVisitors)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventVisitors_User");
            });

            modelBuilder.Entity<Files>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Files__3214EC07CDD25E87");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
                entity.Property(e => e.FileName)
                    .HasMaxLength(255)
                    .IsFixedLength();
                entity.Property(e => e.FileType)
                    .HasMaxLength(255)
                    .IsFixedLength();

                entity.HasOne(d => d.FileContent).WithMany(p => p.Files)
                    .HasForeignKey(d => d.FileContentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_FileContentId");
            });

            modelBuilder.Entity<FileContent>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC071B09BE45");

                entity.ToTable("FileContent");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
                entity.Property(e => e.Base64Content).IsUnicode(false);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Message__3214EC07602A7775");

                entity.ToTable("Message");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Creator).WithMany(p => p.Messages)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_User");

                entity.HasOne(d => d.Event).WithMany(p => p.Messages)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_Event");

                entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Message_Message");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__User__3214EC0722C62A4B");

                entity.ToTable("User");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
                entity.Property(e => e.Email).HasMaxLength(50);
                entity.Property(e => e.Firstname).HasMaxLength(50);
                entity.Property(e => e.Lastname).HasMaxLength(50);
                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.ProfilePicture).WithMany(p => p.Users)
                    .HasForeignKey(d => d.ProfilePictureId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_User_File");
            });

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(true, cancellationToken);
        }
    }
}
