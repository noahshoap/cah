namespace cah.Abstractions;

public interface IPlayerService
{
    public int PlayerCount { get; }
    public Task<IPlayer> CreatePlayer(string contextId, string? playerName);
    public Task<IPlayer> GetPlayer(Guid playerId);
    public Task RemovePlayer(Guid playerId);
    public Task UpdatePlayer(Guid playerId, string? contextId, string? playerName);
    public Task<Guid> PlayerConnected(string? playerId, string connectionId, string? playerName);
}