using System.Collections.Concurrent;
using cah.Abstractions;
using cah.Domain;

namespace cah.Services;

public class PlayerService : IPlayerService
{
    private readonly ConcurrentDictionary<Guid, Player> _players = new();
    public int PlayerCount => _players.Count;
    
    public async Task<IPlayer> CreatePlayer(string contextId, string? playerName)
    {
        var playerId = Guid.NewGuid();
        var player = new Player(playerId, contextId, playerName);
        _players.TryAdd(playerId, player);
        
        return player;
    }

    public async Task RemovePlayer(Guid playerId)
    {
        _players.TryRemove(playerId, out _);
    }

    public async Task UpdatePlayer(Guid playerId, string? contextId = null, string? playerName = null)
    {
        if (!_players.TryGetValue(playerId, out var player)) return;

        if (contextId is not null)
        {
            player.UpdateConnectionId(contextId);
        }

        if (playerName is not null)
        {
            player.UpdateName(playerName);
        }
    }
}