using System.Reflection;
using cah.Domain;
using cah.GameModes;
using cah.Services;

namespace UnitTests;

public class GameFactoryUnitTests
{
    private GameFactory _gameFactory = new();

    [Theory]
    [InlineData("base", typeof(BaseGameMode))]
    [InlineData("democracy", typeof(DemocracyGameMode))]
    public void GameFactory_CreatesCorrectGameMode(string gameMode, Type expectedType)
    {
        // arrange
        var configuration = new GameConfigurationRequest { GameMode = gameMode };
        
        // act
        var game = _gameFactory.CreateGame(configuration);
        
        // assert
        Assert.IsType(expectedType, game);
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