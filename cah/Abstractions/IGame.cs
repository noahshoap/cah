using cah.Domain;

namespace cah.Abstractions;

public interface IGame
{
    public Guid Id { get; }
    public Task AddPlayer(IPlayer player);
    public Task LoadCardSet(ICardSet cardSet);
    public Task StartGame();
}