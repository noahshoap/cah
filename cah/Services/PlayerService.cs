using System.Collections.Concurrent;
using cah.Abstractions;
using cah.Domain;

namespace cah.Services;

public class PlayerService : IPlayerService
{
    private readonly ConcurrentDictionary<string, Player> _players = new();
    
    public async Task AddPlayer(string contextId, string? playerName)
    {
        var player = new Player(contextId, playerName);
        _players.TryAdd(contextId, player);
    }

    public async Task RemovePlayer(string contextId)
    {
        _players.TryRemove(contextId, out _);
    }
}