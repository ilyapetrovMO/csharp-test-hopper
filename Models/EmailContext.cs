using Microsoft.EntityFrameworkCore;

namespace csharp_test_hopper.Models
{
    public class EmailContext : DbContext
    {
        public EmailContext(DbContextOptions<EmailContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EmailEntry> Emails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailEntry>(e =>
            {
                e.Property(e => e.CreatedAt).HasDefaultValueSql("now() at time zone 'utc'");
            });
            
        }
    }
}
