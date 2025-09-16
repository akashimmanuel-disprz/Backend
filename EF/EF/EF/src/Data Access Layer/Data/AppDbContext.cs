using DomainCoreLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Table for events
        public DbSet<EventModel> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Explicitly define EventId as PK
            modelBuilder.Entity<EventModel>()
                .HasKey(e => e.EventId);

            // 2. Configure Recurrence as an owned type
            modelBuilder.Entity<EventModel>()
                .OwnsOne(e => e.Recurrence, r =>
                {
                    r.Property(x => x.RecurrenceType)
                        .HasMaxLength(50)       // Optional: set max length
                        .HasColumnName("RecurrenceType");

                    r.Property(x => x.RecurrenceCount)
                        .HasColumnName("RecurrenceCount");
                });

            // Optional: further configurations, e.g., max lengths for EventModel properties
            modelBuilder.Entity<EventModel>()
                .Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<EventModel>()
                .Property(e => e.Description)
                .HasMaxLength(1000);
        }
    }
}