using LeagueHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeagueHub.Infrastructure.Persistence;

public class LeagueHubDbContext : DbContext
{
    public LeagueHubDbContext(DbContextOptions<LeagueHubDbContext> options) : base(options) { }

    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Match> Matches => Set<Match>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>(entity =>
        {
            entity.Property(t => t.Name).IsRequired().HasMaxLength(200);
            entity.Property(t => t.City).HasMaxLength(200);
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
            entity.Property(p => p.TeamId).IsRequired();
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.Property(m => m.HomeTeamId).IsRequired();
            entity.Property(m => m.AwayTeamId).IsRequired();
        });
    }
}
