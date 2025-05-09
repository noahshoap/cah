using cah.Services;

namespace UnitTests;

public class PlayerServiceUnitTests
{
    private PlayerService _playerService = new PlayerService();

    [Fact]
    public async Task CreatePlayer_ShouldCreatePlayer()
    {
        Assert.Equal(0, _playerService.PlayerCount);
        
        // act
        var player = await _playerService.CreatePlayer("MyContext", "MyPlayer");
        
        // assert
        Assert.NotNull(player);
        Assert.Equal(1, _playerService.PlayerCount);
    }
    
    [Fact]
    public async Task UpdatePlayer_ShouldUpdatePlayer()
    {
        // arrange / assert added
        Assert.Equal(0, _playerService.PlayerCount);
        
        var player = await _playerService.CreatePlayer("MyContext", "MyPlayer");
        
        // assert
        Assert.NotNull(player);
        Assert.Equal(1, _playerService.PlayerCount);
        
        // act
        await _playerService.UpdatePlayer(player.Id, "RejoinedId", "JohnDoe");
        
        // assert
        Assert.Equal(1, _playerService.PlayerCount);
        Assert.Equal("RejoinedId", player.ConnectionId);
        Assert.Equal("JohnDoe", player.Name);
    }

    [Fact]
    public async Task RemovePlayer_ShouldRemovePlayer()
    {
        // arrange / assert added
        Assert.Equal(0, _playerService.PlayerCount);
        
        var player = await _playerService.CreatePlayer("MyContext", "MyPlayer");
        
        // assert
        Assert.NotNull(player);
        Assert.Equal(1, _playerService.PlayerCount);
        
        // act
        await _playerService.RemovePlayer(player.Id);
        Assert.Equal(0, _playerService.PlayerCount);
    }
}