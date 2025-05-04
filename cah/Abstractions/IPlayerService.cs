namespace cah.Abstractions;

public interface IPlayerService
{
    public Task AddPlayer(string contextId, string? playerName);
    public Task RemovePlayer(string contextId);
}