using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Interfaces;
using LeagueHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LeagueHub.Infrastructure.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly LeagueHubDbContext _context;

    public TeamRepository(LeagueHubDbContext context)
    {
        _context = context;
    }

    public async Task<List<Team>> GetAllAsync()
    {
        return await _context.Teams.AsNoTracking().ToListAsync();
    }

    public async Task<Team?> GetByIdAsync(int id)
    {
        return await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Team> AddAsync(Team team)
    {
        _context.Teams.Add(team);
        await _context.SaveChangesAsync();
        return team;
    }
}
