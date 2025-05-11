using cah.Domain;

namespace cah.Abstractions;

public interface IPlayer
{
    public event EventHandler PlayerDisconnected;
    public event EventHandler<PlayerPlayedCardArgs> CardPlayed;
    public Guid Id { get; }
    public string Name { get; }
    public string ConnectionId { get; }
    public uint Points { get; }
    public bool IsDisconnected { get; set; }
    
    public Task UpdateName(string newName);
    public Task UpdateConnectionId(string newConnectionId);
    public Task ReceiveCards(IEnumerable<Card> cards);
    public Task PlayCard(Card card);
    public Task Disconnect();
    public Task<int> GetCardCount();
}