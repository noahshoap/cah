using cah.Domain;

namespace cah.Abstractions;

public interface IGame
{
    public Guid Id { get; }
    public void AddPlayer(string playerName);
    public Task LoadCardSet(ICardSet cardSet);
}