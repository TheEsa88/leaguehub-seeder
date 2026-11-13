using LeagueHub.Domain.Exceptions;

namespace LeagueHub.Domain.Entities;

public class Match
{
    public int Id { get; private set; }
    public int HomeTeamId { get; private set; }
    public int AwayTeamId { get; private set; }
    public DateTime ScheduledAt { get; private set; }

    private Match() { }

    public static Match Create(int homeTeamId, int awayTeamId, DateTime scheduledAt)
    {
        if (homeTeamId == awayTeamId)
        {
            throw new DomainException("Koti ja vieras eivät voi olla sama joukkue.");
        }

        return new Match
        {
            HomeTeamId = homeTeamId,
            AwayTeamId = awayTeamId,
            ScheduledAt = scheduledAt
        };
    }
}
