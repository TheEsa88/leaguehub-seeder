using LeagueHub.Domain.Entities;

namespace LeagueHub.Domain.Interfaces;

public interface IPlayerRepository
{
    Task<List<Player>> GetByTeamIdAsync(int teamId);
    Task<Player> AddAsync(Player player);
}
