namespace cah.Abstractions;

public interface IGameClient
{
    Task GameCreated(string gameId);
    Task JoinedGame(string gameId);
    Task SendError(string errorMessage);
    Task AssignPlayerId(Guid playerId);
}