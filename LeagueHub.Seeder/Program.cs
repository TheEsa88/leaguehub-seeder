using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

JsonSerializerOptions jsonOptions = new()
{
    PropertyNameCaseInsensitive = true
};

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile("appsettings.local.json", optional: true)
    .Build();

string baseUrl = config["ApiBaseUrl"] ?? throw new InvalidOperationException("ApiBaseUrl puuttuu.");

using HttpClient client = new()
{
    BaseAddress = new Uri(baseUrl.TrimEnd('/') + "/")
};

Console.WriteLine($"Kohde {client.BaseAddress}");

TeamDto mikkelinMyrsky = await EnsureTeamAsync("Mikkelin Myrsky", "Mikkeli", 12);
TeamDto kouvolanKiekko = await EnsureTeamAsync("Kouvolan Kiekko", "Kouvola", 12);
TeamDto savonlinnanSahly = await EnsureTeamAsync("Savonlinnan Sähly", "Savonlinna", 10);

await EnsurePlayerAsync(mikkelinMyrsky.Id, "Matti Meikäläinen", 7);
await EnsurePlayerAsync(mikkelinMyrsky.Id, "Teppo Testaaja", 10);
await EnsurePlayerAsync(kouvolanKiekko.Id, "Kaisa Kiekkonen", 7);

await EnsureMatchAsync(mikkelinMyrsky.Id, kouvolanKiekko.Id, DateTime.UtcNow.AddDays(-2));
await EnsureMatchAsync(kouvolanKiekko.Id, savonlinnanSahly.Id, DateTime.UtcNow.AddDays(7));

async Task<TeamDto> EnsureTeamAsync(string name, string city, int maxRoster)
{
    List<TeamDto> teams = await client.GetFromJsonAsync<List<TeamDto>>("api/teams", jsonOptions) ?? [];
    TeamDto? existing = teams.FirstOrDefault(t => t.Name == name);
    if (existing is not null)
    {
        Console.WriteLine($"oli jo  joukkue  {existing.Name}  id={existing.Id}");
        return existing;
    }

    using HttpResponseMessage response = await client.PostAsJsonAsync(
        "api/teams", new { name, city, maxRoster });
    if (!response.IsSuccessStatusCode)
    {
        string body = await response.Content.ReadAsStringAsync();
        throw new InvalidOperationException($"POST api/teams {(int)response.StatusCode} {body}");
    }

    TeamDto created = (await response.Content.ReadFromJsonAsync<TeamDto>(jsonOptions))!;
    Console.WriteLine($"luotiin joukkue  {created.Name}  id={created.Id}");
    return created;
}

async Task<PlayerDto> EnsurePlayerAsync(int teamId, string name, int number)
{
    string path = $"api/teams/{teamId}/players";
    List<PlayerDto> players = await client.GetFromJsonAsync<List<PlayerDto>>(path, jsonOptions) ?? [];
    PlayerDto? existing = players.FirstOrDefault(p => p.Name == name);
    if (existing is not null)
    {
        Console.WriteLine($"oli jo  pelaaja  {existing.Name}  id={existing.Id}");
        return existing;
    }

    using HttpResponseMessage response = await client.PostAsJsonAsync(path, new { name, number });
    if (!response.IsSuccessStatusCode)
    {
        string body = await response.Content.ReadAsStringAsync();
        throw new InvalidOperationException($"POST {path} {(int)response.StatusCode} {body}");
    }

    PlayerDto created = (await response.Content.ReadFromJsonAsync<PlayerDto>(jsonOptions))!;
    Console.WriteLine($"luotiin pelaaja  {created.Name}  id={created.Id}");
    return created;
}

async Task<MatchDto> EnsureMatchAsync(int homeTeamId, int awayTeamId, DateTime scheduledAt)
{
    List<MatchDto> matches = await client.GetFromJsonAsync<List<MatchDto>>("api/matches", jsonOptions) ?? [];
    MatchDto? existing = matches.FirstOrDefault(m => m.HomeTeamId == homeTeamId && m.AwayTeamId == awayTeamId);
    if (existing is not null)
    {
        Console.WriteLine($"oli jo  ottelu  {homeTeamId}-{awayTeamId}  id={existing.Id}");
        return existing;
    }

    using HttpResponseMessage response = await client.PostAsJsonAsync(
        "api/matches", new { homeTeamId, awayTeamId, scheduledAt });
    if (!response.IsSuccessStatusCode)
    {
        string body = await response.Content.ReadAsStringAsync();
        throw new InvalidOperationException($"POST api/matches {(int)response.StatusCode} {body}");
    }

    MatchDto created = (await response.Content.ReadFromJsonAsync<MatchDto>(jsonOptions))!;
    Console.WriteLine($"luotiin ottelu  {homeTeamId}-{awayTeamId}  id={created.Id}");
    return created;
}

record TeamDto(int Id, string Name, string City, int MaxRoster);
record PlayerDto(int Id, string Name, int Number);
record MatchDto(int Id, int HomeTeamId, int AwayTeamId);
