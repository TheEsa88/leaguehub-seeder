using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Interfaces;

namespace LeagueHub.Application.UseCases.Matches;

public class GetMatchesUseCase
{
    private readonly IMatchRepository _matches;

    public GetMatchesUseCase(IMatchRepository matches)
    {
        _matches = matches;
    }

    public Task<List<Match>> ExecuteAsync() => _matches.GetAllAsync();
}
