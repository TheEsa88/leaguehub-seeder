using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Interfaces;
using LeagueHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LeagueHub.Infrastructure.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly LeagueHubDbContext _context;

    public PlayerRepository(LeagueHubDbContext context)
    {
        _context = context;
    }

    public async Task<List<Player>> GetByTeamIdAsync(int teamId)
    {
        return await _context.Players
            .AsNoTracking()
            .Where(p => p.TeamId == teamId)
            .ToListAsync();
    }

    public async Task<Player> AddAsync(Player player)
    {
        _context.Players.Add(player);
        await _context.SaveChangesAsync();
        return player;
    }
}
