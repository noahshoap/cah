using cah.Domain;
using cah.GameModes;
using cah.Services;

namespace UnitTests;

public class GameFactoryUnitTests
{
    private GameFactory _gameFactory = new();

    [Fact]
    public void GameFactory_CreatesBaseGame()
    {
        // arrange
        var configuration = new GameConfigurationRequest { GameMode = "base" };
        
        // act
        var game = _gameFactory.CreateGame(configuration);
        
        // assert
        Assert.IsType<BaseGameMode>(game);
    }

    [Fact]
    public void GameFactory_CreatesDemocracyGame()
    {
        // arrange
        var configuration = new GameConfigurationRequest { GameMode = "democracy" };
        
        // act
        var game = _gameFactory.CreateGame(configuration);
        
        // assert
        Assert.IsType<DemocracyGameMode>(game);
    }

    [Fact]
    public void GameFactory_CreateGame_ThrowsNotImplementedException()
    {
        // arrange
        var configuration = new GameConfigurationRequest { GameMode = "deadbeef" };
        
        // act & assert
        Assert.Throws<NotImplementedException>(() => _gameFactory.CreateGame(configuration));
    }
}