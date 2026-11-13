using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Interfaces;

namespace LeagueHub.Application.UseCases.Teams;

public class CreateTeamUseCase
{
    private readonly ITeamRepository _teams;

    public CreateTeamUseCase(ITeamRepository teams)
    {
        _teams = teams;
    }

    public async Task<Team> ExecuteAsync(string name, string city, int maxRoster)
    {
        Team team = Team.Create(name, city, maxRoster);
        return await _teams.AddAsync(team);
    }
}
