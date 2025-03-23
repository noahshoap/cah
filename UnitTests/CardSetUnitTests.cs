using cah.Domain;

namespace UnitTests;

public class CardSetUnitTests
{
    private CardSet _cardSet = new();

    [Fact]
    public void CardSet_AddCard_ShouldAddCard()
    {
        // arrange
        var card = new Card(0, "hi", CardType.Answer, "base");
        
        // act
        _cardSet.AddCard(card);
        
        // assert
        Assert.NotEmpty(_cardSet.GetCards());
    }

    [Fact]
    public void CardSet_GetCard_ShouldReturnEmpty()
    {
        Assert.Empty(_cardSet.GetCards());
    }
}