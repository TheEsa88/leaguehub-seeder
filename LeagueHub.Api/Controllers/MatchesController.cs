using LeagueHub.Api.Requests;
using LeagueHub.Application.Exceptions;
using LeagueHub.Application.UseCases.Matches;
using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LeagueHub.Api.Controllers;

[ApiController]
[Route("api/v1/matches")]
public class MatchesController : ControllerBase
{
    private readonly GetMatchesUseCase _getMatches;
    private readonly CreateMatchUseCase _createMatch;

    public MatchesController(GetMatchesUseCase getMatches, CreateMatchUseCase createMatch)
    {
        _getMatches = getMatches;
        _createMatch = createMatch;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _getMatches.ExecuteAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMatchRequest request)
    {
        try
        {
            Match created = await _createMatch.ExecuteAsync(
                request.HomeTeamId, request.AwayTeamId, request.ScheduledAt);

            return Created($"/api/v1/matches/{created.Id}", created);
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
