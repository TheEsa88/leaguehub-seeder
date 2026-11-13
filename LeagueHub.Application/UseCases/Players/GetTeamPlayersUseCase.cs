using LeagueHub.Application.Exceptions;
using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Interfaces;

namespace LeagueHub.Application.UseCases.Players;

public class GetTeamPlayersUseCase
{
    private readonly ITeamRepository _teams;
    private readonly IPlayerRepository _players;

    public GetTeamPlayersUseCase(ITeamRepository teams, IPlayerRepository players)
    {
        _teams = teams;
        _players = players;
    }

    public async Task<List<Player>> ExecuteAsync(int teamId)
    {
        if (await _teams.GetByIdAsync(teamId) is null)
        {
            throw new NotFoundException("Joukkuetta ei löydy.");
        }

        return await _players.GetByTeamIdAsync(teamId);
    }
}
