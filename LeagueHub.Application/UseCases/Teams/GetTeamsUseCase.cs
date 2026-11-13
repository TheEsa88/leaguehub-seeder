using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Interfaces;

namespace LeagueHub.Application.UseCases.Teams;

public class GetTeamsUseCase
{
    private readonly ITeamRepository _teams;

    public GetTeamsUseCase(ITeamRepository teams)
    {
        _teams = teams;
    }

    public Task<List<Team>> ExecuteAsync() => _teams.GetAllAsync();
}
