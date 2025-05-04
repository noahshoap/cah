namespace cah.Domain;

public class Player
{
    public Player(string connectionId, string? playerName)
    {
        ConnectionId = connectionId;
        Name = playerName ?? "Player";
    }
    
    public string Name { get; set; }
    public string ConnectionId { get; set; }
}