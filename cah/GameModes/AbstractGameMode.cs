using cah.Domain;

namespace cah.Abstractions;

public abstract class AbstractGameMode : IGame
{
    protected Queue<Card> QuestionCards { get; set; } = new();
    protected Queue<Card> AnswerCards { get; set; } = new();
    private HashSet<IPlayer> Players { get; set; } = new();
    private bool _started;
    public Guid Id { get; }

    public AbstractGameMode(Guid id, IEnumerable<Card> cards)
    {
        Id = id;
        LoadCards(cards);
    }
    
    public async Task StartGame()
    {
        if (_started) return;
        _started = true;
        
    }
    
    public async Task AddPlayer(IPlayer player)
    {
        player.PlayerDisconnected += RemoveDisconnectedPlayer;
        player.CardPlayed += PlayerPlayedCard;
        Players.Add(player);
    }

    private async Task DealCards()
    {
        foreach (var player in Players)
        {
            var playerCardCount = await player.GetCardCount();
            var cardsNeeded = (1 - playerCardCount);
            var cardsToDeal = new List<Card>();
            
            for (var i = 0; i < cardsNeeded; i++)
            {
                cardsToDeal.Add(AnswerCards.Dequeue());
            }

            await player.ReceiveCards(cardsToDeal);
        }
    }
    
    private async Task LoadCards(IEnumerable<Card> cards)
    {
        foreach (var card in cards)
        {
            switch (card.Type)
            {
                case CardType.Question:
                    QuestionCards.Enqueue(card);
                    break;
                case CardType.Answer:
                    AnswerCards.Enqueue(card);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown card type: {card.Type}");
            }
        }
    }
    
    private void RemoveDisconnectedPlayer(object? sender, EventArgs e)
    {
        Players.Remove((IPlayer)sender);
    }

    private void PlayerPlayedCard(object? sender, PlayerPlayedCardArgs e)
    {
        
    }
}