namespace cah.Abstractions;

public interface IPlayerService
{
    public int PlayerCount { get; }
    public Task<IPlayer> CreatePlayer(string contextId, string? playerName);
    public Task RemovePlayer(Guid playerId);
    public Task UpdatePlayer(Guid playerId, string? contextId, string? playerName);
}