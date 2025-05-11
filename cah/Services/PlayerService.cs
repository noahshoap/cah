using System.Collections.Concurrent;
using cah.Abstractions;
using cah.Domain;

namespace cah.Services;

public class PlayerService : IPlayerService
{
    private const uint DISCONNECT_REMOVE_DELAY_MINUTES = 3;
    private readonly ConcurrentDictionary<Guid, IPlayer> _players = new();
    private readonly ConcurrentDictionary<string, IPlayer> _playersByConnection = new();
    public int PlayerCount => _players.Count;
    
    public async Task<IPlayer> CreatePlayer(string contextId, string? playerName)
    {
        var playerId = Guid.NewGuid();
        var player = new Player(playerId, contextId, playerName);
        _players.TryAdd(playerId, player);
        _playersByConnection.TryAdd(contextId, player);
        
        return player;
    }

    public async Task<IPlayer> GetPlayer(Guid playerId)
    {
        if (!_players.TryGetValue(playerId, out var player))
            throw new InvalidOperationException($"Player with id {playerId} was not found.");
        
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

    public async Task<Guid> PlayerConnected(string? playerId, string connectionId, string? playerName)
    {
        IPlayer player;
        
        if (string.IsNullOrEmpty(playerId))
        {
            player = await CreatePlayer(connectionId, playerName ?? "Player");
        }
        else
        {
            // An exception could still get thrown here, but we are assuming that the client only ever passes null/empty or correctly formatted GUIDs.
            // Considering I am the only one who will use this, I am fine with that assumption for now.
            var playerIdGuid = Guid.Parse(playerId);
            player = await GetPlayer(playerIdGuid);
            
            _playersByConnection.TryRemove(player.ConnectionId, out _);
            
            await UpdatePlayer(playerIdGuid, connectionId, playerName);
            
            _playersByConnection.TryAdd(player.ConnectionId, player);
        }
        
        return player.Id;
    }

    public async Task PlayerDisconnected(string connectionId)
    {
        if (!_playersByConnection.TryGetValue(connectionId, out var player)) return;

        await Task.Delay(TimeSpan.FromMinutes(DISCONNECT_REMOVE_DELAY_MINUTES));

        if (player.IsDisconnected)
        {
            // TODO:
            // We need to remove the player from the game.
            // Do that manually or use events?
            _players.TryRemove(player.Id, out _);
            _playersByConnection.TryRemove(player.ConnectionId, out _);
        }
    }
}