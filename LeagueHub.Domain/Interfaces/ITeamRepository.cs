using LeagueHub.Domain.Entities;

namespace LeagueHub.Domain.Interfaces;

public interface ITeamRepository
{
    Task<List<Team>> GetAllAsync();
    Task<Team?> GetByIdAsync(int id);
    Task<Team> AddAsync(Team team);
}
