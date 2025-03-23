using cah.Abstractions;
using cah.Domain;
using cah.GameModes;

namespace cah.Services;

public class GameFactory : IGameFactory
{
    public IGame CreateGame(GameConfigurationRequest configuration)
    {
        var gameId = Guid.NewGuid();
        
        IGame gameInstance = configuration.GameMode switch
        {
            "base" => new BaseGameMode(gameId),
            "democracy" => new DemocracyGameMode(gameId),
            _ => throw new NotImplementedException($"Game mode {configuration.GameMode} does not exist."),
        };

        return gameInstance;
    }
}