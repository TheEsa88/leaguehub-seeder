using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Interfaces;
using LeagueHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LeagueHub.Infrastructure.Repositories;

public class MatchRepository : IMatchRepository
{
    private readonly LeagueHubDbContext _context;

    public MatchRepository(LeagueHubDbContext context)
    {
        _context = context;
    }

    public async Task<List<Match>> GetAllAsync()
    {
        return await _context.Matches.AsNoTracking().ToListAsync();
    }

    public async Task<Match> AddAsync(Match match)
    {
        _context.Matches.Add(match);
        await _context.SaveChangesAsync();
        return match;
    }
}
