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
        await Clients.User(Context.ConnectionId).GameCreated(game.Id.ToString());
    }

    public async Task JoinGame(string gameId)
    {
        var gameFound = _games.TryGetValue(gameId, out var game);

        if (gameFound == false)
        {
            await Clients.Client(Context.ConnectionId).SendError($"A game with id {gameId} was not found.");
            return;
        }
        
        game.AddPlayer(Context.ConnectionId);
        
        await Clients.Client(Context.ConnectionId).JoinedGame($"Successfully joined game {gameId}");
    }
}