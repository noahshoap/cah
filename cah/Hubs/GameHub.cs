using cah.Abstractions;
using cah.Domain;
using Microsoft.AspNetCore.SignalR;

namespace cah.Hubs;

public class GameHub : Hub<IGameClient>
{
    private static Dictionary<string, IGame> _games = new();
    
    public async Task CreateGame(IGameFactory factory)
    {
        // TODO: Get a game configuration request from the client, for now just use the defaults.
        var game = factory.CreateGame(new GameConfigurationRequest());
        
        _games.Add(game.Id.ToString(), game);
        await Clients.Client(Context.ConnectionId).GameCreated(game.Id.ToString());
    }

    public async Task JoinGame(IPlayerService playerService, string gameId, string playerName = "JohnDoe")
    {
        var gameFound = _games.TryGetValue(gameId, out var game);

        if (gameFound == false)
        {
            await Clients.Client(Context.ConnectionId).SendError($"A game with id {gameId} was not found.");
            return;
        }
        
        var player = await playerService.CreatePlayer(Context.ConnectionId, playerName);
        game.AddPlayer(player);
        
        await Clients.Client(Context.ConnectionId).JoinedGame($"Successfully joined game {gameId}");
    }
}