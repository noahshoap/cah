using cah.Abstractions;
using cah.Domain;

namespace UnitTests;

internal class ConcreteGameMode(Guid id, IEnumerable<Card> cards) : AbstractGameMode(id, cards)
{
    public IEnumerable<Card> GetQuestionCards()
    {
        return QuestionCards;
    }

    public IEnumerable<Card> GetAnswerCards()
    {
        return AnswerCards;
    }
};

public class AbstractGameModeUnitTests
{
    private ConcreteGameMode _sut;

    public AbstractGameModeUnitTests()
    {
        _sut = new ConcreteGameMode(Guid.NewGuid(), GetCards());
    }

    [Fact]
    public void AbstractGameMode_Loads_Cards()
    {
        // act
        var questionCards = _sut.GetQuestionCards();
        var answerCards = _sut.GetAnswerCards();
        
        // assert
        Assert.NotEmpty(questionCards);
        Assert.NotEmpty(answerCards);
    }
    
    private IEnumerable<Card> GetCards()
    {
        var cards = new List<Card>
        {
            new Card(0, "hi", CardType.Answer, "base"),
            new Card(1, "bye", CardType.Answer, "base"),
            new Card(2, "hey", CardType.Answer, "base"),
            new Card(3, "hello?", CardType.Question, "base"),
        };

        return cards;
    }
}