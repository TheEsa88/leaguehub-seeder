using LeagueHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeagueHub.Infrastructure.Persistence;

// OSIO 9: tämä tiedosto poistuu, kun käynnistys ei enää lisää demodataa.
// Migraatio jää API:n Program.cs:ään.
public static class DbSeeder
{
    public static async Task SeedAsync(LeagueHubDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Teams.AnyAsync())
        {
            return;
        }

        Team myrsky = Team.Create("Mikkelin Myrsky", "Mikkeli", 12);
        Team kiekko = Team.Create("Kouvolan Kiekko", "Kouvola", 12);
        Team sahly = Team.Create("Savonlinnan Sähly", "Savonlinna", 10);

        context.Teams.AddRange(myrsky, kiekko, sahly);
        await context.SaveChangesAsync();

        context.Players.AddRange(
            Player.Create(myrsky.Id, "Matti Meikäläinen", 7),
            Player.Create(myrsky.Id, "Teppo Testaaja", 10),
            Player.Create(kiekko.Id, "Kaisa Kiekkonen", 7));

        context.Matches.AddRange(
            Match.Create(myrsky.Id, kiekko.Id, DateTime.UtcNow.AddDays(-2)),
            Match.Create(kiekko.Id, sahly.Id, DateTime.UtcNow.AddDays(7)));

        await context.SaveChangesAsync();
    }
}
