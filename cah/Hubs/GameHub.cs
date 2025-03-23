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

        game.AddPlayer(Context.ConnectionId);
        
        await Clients.User(Context.ConnectionId).GameCreated(game.Id.ToString());
    }
}