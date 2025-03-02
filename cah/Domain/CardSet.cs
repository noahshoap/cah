using cah.Abstractions;

namespace cah.Domain;

public class CardSet : ICardSet
{
    private readonly List<Card> _cards = [];

    public void AddCard(Card card)
    {
        _cards.Add(card);
    }

    public IEnumerable<Card> GetCards()
    {
        return _cards;
    }
}