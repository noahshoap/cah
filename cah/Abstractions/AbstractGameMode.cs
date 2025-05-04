using cah.Domain;

namespace cah.Abstractions;

public abstract class AbstractGameMode(Guid id) : IGame
{
    protected List<Card> QuestionCards { get; set; } = new();
    protected List<Card> AnswerCards { get; set; } = new();
    private Dictionary<string, string> Players = new();
    public Guid Id { get; } = id;

    public void AddPlayer(string playerName)
    {
        Players.Add(playerName, playerName);
    }

    public async Task LoadCardSet(ICardSet cardSet)
    {
        var cards = cardSet.GetCards();
        var questionCards = cards.Where(c => c.Type == CardType.Question);
        var answerCards = cards.Where(c => c.Type == CardType.Answer);
        
        QuestionCards.AddRange(questionCards);
        AnswerCards.AddRange(answerCards);
    }
}