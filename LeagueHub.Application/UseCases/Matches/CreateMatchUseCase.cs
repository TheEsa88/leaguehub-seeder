using LeagueHub.Application.Exceptions;
using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Interfaces;

namespace LeagueHub.Application.UseCases.Matches;

public class CreateMatchUseCase
{
    private readonly ITeamRepository _teams;
    private readonly IMatchRepository _matches;

    public CreateMatchUseCase(ITeamRepository teams, IMatchRepository matches)
    {
        _teams = teams;
        _matches = matches;
    }

    public async Task<Match> ExecuteAsync(int homeTeamId, int awayTeamId, DateTime scheduledAt)
    {
        if (await _teams.GetByIdAsync(homeTeamId) is null
            || await _teams.GetByIdAsync(awayTeamId) is null)
        {
            throw new NotFoundException("Molempien joukkueiden pitää olla olemassa.");
        }

        Match match = Match.Create(homeTeamId, awayTeamId, scheduledAt);
        return await _matches.AddAsync(match);
    }
}
