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
            "base" => new BaseGameMode(gameId, configuration.Cards),
            "democracy" => new DemocracyGameMode(gameId, configuration.Cards),
            _ => throw new NotImplementedException($"Game mode {configuration.GameMode} does not exist."),
        };

        return gameInstance;
    }
}