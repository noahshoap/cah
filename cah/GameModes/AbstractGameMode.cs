using cah.Domain;

namespace cah.Abstractions;

public abstract class AbstractGameMode(Guid id) : IGame
{
    protected HashSet<Card> QuestionCards { get; set; } = new();
    protected HashSet<Card> AnswerCards { get; set; } = new();
    private List<IPlayer> Players { get; set; } = new();
    private bool _started;
    public Guid Id { get; } = id;

    public async Task StartGame()
    {
        if (_started) return;
        _started = true;
        
    }
    
    public async Task AddPlayer(IPlayer player)
    {
        Players.Add(player);
    }

    // TODO: This method should just use `ICardService` and call `GetCardsFromSets`.
    public async Task LoadCardSet(ICardSet cardSet)
    {
        var cards = cardSet.GetCards();

        foreach (var card in cards)
        {
            switch (card.Type)
            {
                case CardType.Question:
                    QuestionCards.Add(card);
                    break;
                case CardType.Answer:
                    AnswerCards.Add(card);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown card type: {card.Type}");
            }
        }
    }
}