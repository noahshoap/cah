using System;
using System.Threading.Tasks;
using cah.Abstractions;
using cah.Domain;
using cah.Hubs;
using FakeItEasy;
using Microsoft.AspNetCore.SignalR;
using Xunit;

public class GameHubTests
{
    private string _connectionId;
    private Guid _gameId;
    private IGameFactory _fakeGameFactory;
    private IGameClient _fakeGameClient;
    private IGame _fakeGame;
    private HubCallerContext _fakeHubCallerContext;
    private IHubCallerClients<IGameClient> _fakeClients;

    public GameHubTests()
    {
        _fakeGameFactory = A.Fake<IGameFactory>();
        _fakeGameClient = A.Fake<IGameClient>();
        _fakeGame = A.Fake<IGame>();
        _fakeHubCallerContext = A.Fake<HubCallerContext>();
        _fakeClients = A.Fake<IHubCallerClients<IGameClient>>();
        
        _connectionId = Guid.NewGuid().ToString();
        _gameId = Guid.NewGuid();
        
        A.CallTo(() => _fakeHubCallerContext.ConnectionId).Returns(_connectionId);
        A.CallTo(() => _fakeGame.Id).Returns(_gameId);
        A.CallTo(() => _fakeClients.User(_connectionId)).Returns(_fakeGameClient);
        A.CallTo(() => _fakeGameFactory.CreateGame(A<GameConfigurationRequest>.Ignored))
            .Returns(_fakeGame);
        
    }
    
    [Fact]
    public async Task CreateGame_CreatesGame_NotifiesClient()
    {
        // arrange
        var hub = new GameHub
        {
            Clients = _fakeClients,
            Context = _fakeHubCallerContext,
        };

        // act
        await hub.CreateGame(_fakeGameFactory);

        // assert
        A.CallTo(() => _fakeGameFactory.CreateGame(A<GameConfigurationRequest>.Ignored))
            .MustHaveHappenedOnceExactly(); // Ensure the factory was used

        A.CallTo(() => _fakeGameClient.GameCreated(_gameId.ToString()))
            .MustHaveHappenedOnceExactly(); // Ensure the client was notified
    }
}