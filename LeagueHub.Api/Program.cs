using LeagueHub.Application.UseCases.Matches;
using LeagueHub.Application.UseCases.Players;
using LeagueHub.Application.UseCases.Teams;
using LeagueHub.Domain.Interfaces;
using LeagueHub.Infrastructure.Persistence;
using LeagueHub.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddDbContext<LeagueHubDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IMatchRepository, MatchRepository>();

builder.Services.AddScoped<GetTeamsUseCase>();
builder.Services.AddScoped<CreateTeamUseCase>();
builder.Services.AddScoped<GetTeamPlayersUseCase>();
builder.Services.AddScoped<AddPlayerToTeamUseCase>();
builder.Services.AddScoped<GetMatchesUseCase>();
builder.Services.AddScoped<CreateMatchUseCase>();

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    LeagueHubDbContext db = scope.ServiceProvider.GetRequiredService<LeagueHubDbContext>();
    await db.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapHealthChecks("/health");
app.Run();
