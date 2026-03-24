using Microsoft.EntityFrameworkCore;
using LivelySheets.MatchupService.Domain.Entities.Messages;

namespace LivelySheets.MatchupService.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<InboxMessage> InboxMessages { get; set; }
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InboxMessage>()
            .Property(m => m.UpdatedOn)
            .IsRequired(false);
    }
}
