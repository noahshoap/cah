namespace cah.Abstractions;

public class AbstractGameMode(Guid id) : IGame
{
    protected Dictionary<string, string> Players = new();
    public Guid Id { get; } = id;

    public void AddPlayer(string playerName)
    {
        Players.Add(playerName, playerName);
    }
}