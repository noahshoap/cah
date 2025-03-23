namespace cah.Abstractions;

public abstract class AbstractGameMode(Guid id) : IGame
{
    protected Dictionary<string, string> Players = new();
    public Guid Id { get; } = id;

    public void AddPlayer(string playerName)
    {
        Players.Add(playerName, playerName);
    }
}