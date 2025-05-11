using cah.Abstractions;
using cah.Domain;
using Microsoft.AspNetCore.SignalR;

namespace cah.Hubs;

public class GameHub : Hub<IGameClient>
{
    private static Dictionary<string, IGame> _games = new();
    private IPlayerService _playerService;
    
    public GameHub(IPlayerService playerService)
    {
        _playerService = playerService;
    }
    
    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var playerIdFromClient = httpContext?.Request.Query["playerId"].FirstOrDefault();
        var playerNameFromClient = httpContext?.Request.Query["playerName"].FirstOrDefault();

        if (string.IsNullOrWhiteSpace(playerIdFromClient))
        {
            var player = await _playerService.CreatePlayer(Context.ConnectionId, playerNameFromClient ?? "Player");
            await Clients.Caller.AssignPlayerId(player.Id);
        }
        else
        {
            var guidId = Guid.Parse(playerIdFromClient);
            await _playerService.UpdatePlayer(guidId, Context.ConnectionId, playerNameFromClient);
        }
        
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        // We need to add a grace period to allow the player to reconnect.
        await base.OnDisconnectedAsync(exception);
    }
    
    public async Task CreateGame(IGameFactory factory)
    {
        // TODO: Get a game configuration request from the client, for now just use the defaults.
        var game = factory.CreateGame(new GameConfigurationRequest());
        
        _games.Add(game.Id.ToString(), game);
        await Clients.Client(Context.ConnectionId).GameCreated(game.Id.ToString());
    }

    public async Task JoinGame(string gameId)
    {
        var gameFound = _games.TryGetValue(gameId, out var game);

        if (gameFound == false)
        {
            await Clients.Client(Context.ConnectionId).SendError($"A game with id {gameId} was not found.");
            return;
        }
        
        var player = await _playerService.GetPlayer(GetPlayerIdFromContext());
        game.AddPlayer(player);
        
        await Clients.Client(Context.ConnectionId).JoinedGame($"Successfully joined game {gameId}");
    }

    private Guid GetPlayerIdFromContext()
    {
        var httpContext = Context.GetHttpContext();
        var playerIdFromClient = httpContext?.Request.Query["playerId"].FirstOrDefault();
        
        return Guid.Parse(playerIdFromClient);
    }
}