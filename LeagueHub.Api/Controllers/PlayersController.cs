using LeagueHub.Api.Requests;
using LeagueHub.Application.Exceptions;
using LeagueHub.Application.UseCases.Players;
using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LeagueHub.Api.Controllers;

[ApiController]
[Route("api/v1/teams/{teamId}/players")]
public class PlayersController : ControllerBase
{
    private readonly GetTeamPlayersUseCase _getPlayers;
    private readonly AddPlayerToTeamUseCase _addPlayer;

    public PlayersController(GetTeamPlayersUseCase getPlayers, AddPlayerToTeamUseCase addPlayer)
    {
        _getPlayers = getPlayers;
        _addPlayer = addPlayer;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int teamId)
    {
        try
        {
            return Ok(await _getPlayers.ExecuteAsync(teamId));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add(int teamId, AddPlayerRequest request)
    {
        try
        {
            Player created = await _addPlayer.ExecuteAsync(teamId, request.Name, request.Number);
            return Created($"/api/v1/teams/{teamId}/players/{created.Id}", created);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
