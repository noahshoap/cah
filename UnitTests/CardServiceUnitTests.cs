using System.Text.Json;
using cah.Domain;
using cah.Services;

namespace UnitTests;

public class CardServiceUnitTests : IDisposable
{
    private CardService _sut;
    private readonly string TEST_FILE_NAME = "test_cards.json";
    
    public CardServiceUnitTests()
    {
        _sut = new CardService();
        CreateTestCardFile();
    }
    
    [Fact]
    public void GetCardSets_Returns_Created_CardSets()
    {
        // arrange
        _sut.LoadCards(TEST_FILE_NAME);
        
        // act
        var cardSets = _sut.GetCardSets();
        
        // assert
        Assert.NotEmpty(cardSets);
    }

    [Fact]
    public void GetCardsFromSets_Returns_Cards()
    {
        // arrange
        _sut.LoadCards(TEST_FILE_NAME);
        
        var sets = _sut.GetCardSets();
        var usedSets = sets.Take(2);
        
        // act
        var cards = _sut.GetCardsFromSets(usedSets);
        
        // assert
        Assert.NotEmpty(cards);
    }
    
    private void CreateTestCardFile()
    {
        var cards = new List<Card>
        {
            new Card(0, "hi", CardType.Answer, "base"),
            new Card(1, "hello?", CardType.Question, "base"),
        };
        
        var jsonContent = JsonSerializer.Serialize(cards);
        
        File.WriteAllText(TEST_FILE_NAME, jsonContent);
    }
    
    public void Dispose()
    {
        File.Delete(TEST_FILE_NAME);
    }
}