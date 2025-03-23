namespace cah.Abstractions;

public interface IGameClient
{
    Task GameCreated(string gameId);
}