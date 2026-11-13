using LeagueHub.Domain.Exceptions;

namespace LeagueHub.Domain.Entities;

public class Player
{
    public int Id { get; private set; }
    public int TeamId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int Number { get; private set; }

    private Player() { }

    public static Player Create(int teamId, string name, int number)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Pelaajan nimi on pakollinen.");
        }

        if (number <= 0)
        {
            throw new DomainException("Pelinumeron pitää olla positiivinen.");
        }

        return new Player
        {
            TeamId = teamId,
            Name = name.Trim(),
            Number = number
        };
    }
}
