using LeagueHub.Domain.Entities;

namespace LeagueHub.Domain.Interfaces;

public interface IMatchRepository
{
    Task<List<Match>> GetAllAsync();
    Task<Match> AddAsync(Match match);
}
