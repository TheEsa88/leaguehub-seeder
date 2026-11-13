using LeagueHub.Api.Requests;
using LeagueHub.Application.UseCases.Teams;
using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LeagueHub.Api.Controllers;

[ApiController]
[Route("api/teams")] // OSIO 10: vaihda polku muotoon api/v1/teams. Päivitä myös Created-osoite.
public class TeamsController : ControllerBase
{
    private readonly GetTeamsUseCase _getTeams;
    private readonly CreateTeamUseCase _createTeam;

    public TeamsController(GetTeamsUseCase getTeams, CreateTeamUseCase createTeam)
    {
        _getTeams = getTeams;
        _createTeam = createTeam;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _getTeams.ExecuteAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTeamRequest request)
    {
        try
        {
            Team created = await _createTeam.ExecuteAsync(request.Name, request.City, request.MaxRoster);
            return Created($"/api/teams/{created.Id}", created);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
