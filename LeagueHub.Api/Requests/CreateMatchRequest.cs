namespace LeagueHub.Api.Requests;

public record CreateMatchRequest(int HomeTeamId, int AwayTeamId, DateTime ScheduledAt);
