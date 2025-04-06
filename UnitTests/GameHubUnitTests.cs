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
    private const uint DEFAULT_CLIENTS = 3;
    private List<string> _connectionIds = new();
    private Guid _gameId;
    private IGameFactory _fakeGameFactory;
    private Dictionary<string, IGameClient> _fakeGameClients = new();
    private IGame _fakeGame;
    private Dictionary<string, HubCallerContext> _fakeHubCallerContexts = new();
    private IHubCallerClients<IGameClient> _fakeClients;

    public GameHubTests()
    {
        _fakeGameFactory = A.Fake<IGameFactory>();
        _fakeGame = A.Fake<IGame>();
        _fakeClients = A.Fake<IHubCallerClients<IGameClient>>();

        for (var i = 0; i < DEFAULT_CLIENTS; i++)
        {
            var connectionId = Guid.NewGuid().ToString();
            _connectionIds.Add(connectionId);
            
            var fakeClient = A.Fake<IGameClient>();
            _fakeGameClients.Add(connectionId, fakeClient);
            
            var fakeHubCallerContext = A.Fake<HubCallerContext>();
            _fakeHubCallerContexts.Add(connectionId, fakeHubCallerContext);
            
            A.CallTo(() => fakeHubCallerContext.ConnectionId).Returns(connectionId);
            A.CallTo(() => _fakeClients.Client(connectionId)).Returns(fakeClient);
        }
        
        _gameId = Guid.NewGuid();
        
        A.CallTo(() => _fakeGame.Id).Returns(_gameId);
        A.CallTo(() => _fakeGameFactory.CreateGame(A<GameConfigurationRequest>.Ignored))
            .Returns(_fakeGame);
        
    }
    
    [Fact]
    public async Task CreateGame_CreatesGame_NotifiesClient()
    {
        // This has been moved to a private helper method to avoid duplicating code.
        // This scenario should happen for the bulk of the tests, so I didn't want to rewrite this code over and over again.
        PlayerCreatesLobby();
    }

    [Fact]
    public async Task JoinGame_Fails_If_Game_NotFound()
    {
        var connectionId = _connectionIds.First();
        var fakeHubCallerContext = _fakeHubCallerContexts[connectionId];
        var fakeGameClient = _fakeGameClients[connectionId];
        
        // arrange
        var hub = new GameHub
        {
            Clients = _fakeClients,
            Context = fakeHubCallerContext,
        };

        // act
        await hub.JoinGame(_gameId.ToString());

        // assert
        A.CallTo(() => _fakeGameFactory.CreateGame(A<GameConfigurationRequest>.Ignored))
            .MustNotHaveHappened(); // Ensure no game was created

        A.CallTo(() => fakeGameClient.JoinedGame(A<string>.Ignored))
            .MustNotHaveHappened(); // Ensure client does not receive a joined game notification
        
        A.CallTo(() => fakeGameClient.SendError(A<string>.Ignored))
            .MustHaveHappenedOnceExactly(); // Ensure the client was notified of an error
    }

    [Fact]
    public async Task AllClients_JoinGame_Succeeds()
    {
        PlayerCreatesLobby();

        foreach (var connectionId in _connectionIds)
        {
            var fakeHubCallerContext = _fakeHubCallerContexts[connectionId];
            var fakeGameClient = _fakeGameClients[connectionId];
        
            // arrange
            var hub = new GameHub
            {
                Clients = _fakeClients,
                Context = fakeHubCallerContext,
            };

            // act
            await hub.JoinGame(_gameId.ToString());

            // assert
            A.CallTo(() => fakeGameClient.JoinedGame(A<string>.Ignored))
                .MustHaveHappenedOnceExactly(); // Ensure client joins
        }
    }
    
    private async Task PlayerCreatesLobby()
    {
        var connectionId = _connectionIds.First();
        var fakeHubCallerContext = _fakeHubCallerContexts[connectionId];
        var fakeGameClient = _fakeGameClients[connectionId];
        
        // arrange
        var hub = new GameHub
        {
            Clients = _fakeClients,
            Context = fakeHubCallerContext,
        };

        // act
        await hub.CreateGame(_fakeGameFactory);

        // assert
        A.CallTo(() => _fakeGameFactory.CreateGame(A<GameConfigurationRequest>.Ignored))
            .MustHaveHappenedOnceExactly(); // Ensure the factory was used

        A.CallTo(() => fakeGameClient.GameCreated(_gameId.ToString()))
            .MustHaveHappenedOnceExactly(); // Ensure the client was notified
    }
}