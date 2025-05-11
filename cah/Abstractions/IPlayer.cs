using cah.Domain;

namespace cah.Abstractions;

public interface IPlayer
{
    public event EventHandler<PlayerArgs> PlayerDisconnected;
    public event EventHandler<PlayerPlayedCardArgs> CardPlayed;
    public Guid Id { get; }
    public string Name { get; }
    public string ConnectionId { get; }
    public uint Points { get; }
    public bool IsDisconnected { get; set; }
    
    public Task UpdateName(string newName);
    public Task UpdateConnectionId(string newConnectionId);
    public Task DealCard(Card card);
    public Task PlayCard(Card card);
    public Task RemoveFromGame();
    public Task<int> GetCardCount();
}