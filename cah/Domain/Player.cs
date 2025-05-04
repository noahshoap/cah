using cah.Abstractions;

namespace cah.Domain;

public class Player : IPlayer
{
    private List<Card> _cards = new();
    public string Name { get; private set; }
    public string ConnectionId { get; private set; }
    public uint Points { get; }
    
    public Player(string connectionId, string? playerName)
    {
        ConnectionId = connectionId;
        Name = playerName ?? "Player";
    }
    
    public async Task UpdateName(string newName)
    {
        // TODO: Consider just making the setter public?  I'm not sure.  I like the explicitness of this for this use case.
        Name = newName;
    }

    public async Task UpdateConnectionId(string newConnectionId)
    {
        // TODO: Consider just making the setter public?  I'm not sure.  I like the explicitness of this for this use case.
        ConnectionId = newConnectionId;
    }

    public async Task DealCard(Card card)
    {
        _cards.Add(card);
    }
}