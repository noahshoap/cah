using cah.Domain;

namespace cah.Abstractions;

public interface IPlayer
{
    public string Name { get; }
    public string ConnectionId { get; }
    public uint Points { get; }
    
    public Task UpdateName(string newName);
    public Task UpdateConnectionId(string newConnectionId);
    public Task DealCard(Card card);
    public Task PlayCard(Card card);
    public Task<int> GetCardCount();
}