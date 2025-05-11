using cah.Abstractions;

namespace cah.Domain;

public class Player : IPlayer
{
    private HashSet<Card> _cards = new();
    public event EventHandler<PlayerArgs> PlayerDisconnected;
    public event EventHandler<PlayerPlayedCardArgs> CardPlayed;
    public Guid Id { get;}
    public string Name { get; private set; }
    public string ConnectionId { get; private set; }
    public uint Points { get; }
    public bool IsDisconnected { get; set; }

    public Player(Guid playerId, string connectionId, string? playerName)
    {
        Id = playerId;
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

    public async Task PlayCard(Card card)
    {
        if (_cards.Contains(card))
        {
            _cards.Remove(card);
        }
        
        CardPlayed.Invoke(this, new PlayerPlayedCardArgs(this, card));
    }

    public async Task RemoveFromGame()
    {
        PlayerDisconnected.Invoke(this, new PlayerArgs(this));
    }

    public async Task<int> GetCardCount()
    {
        return _cards.Count;
    }
}