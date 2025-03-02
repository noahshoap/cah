using System.Text.Json;
using cah.Abstractions;
using cah.Domain;

namespace cah.Services;

public class CardService : ICardService
{
    private Dictionary<string, ICardSet> _cardSets = new();
    
    public void LoadCards(string path)
    {
        var content = File.ReadAllText(path);
        var cards = JsonSerializer.Deserialize<List<Card>>(content);

        foreach (var card in cards)
        {
            var set = GetOrCreateCardSet(card.CardSetName);
            set.AddCard(card);
        }
    }

    public IEnumerable<ICardSet> GetCardSets()
    {
        return _cardSets.Values;
    }

    public IEnumerable<Card> GetCardsFromSets(IEnumerable<ICardSet> cardSets)
    {
        return cardSets.SelectMany(cs => cs.GetCards());
    }

    private CardSet GetOrCreateCardSet(string name)
    {
        if (_cardSets.TryGetValue(name, out var cardSet))
        {
            return (CardSet)cardSet;
        }
        
        var set = new CardSet();
        _cardSets.Add(name, set);
        return set;
    }
}