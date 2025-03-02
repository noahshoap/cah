using cah.Abstractions;
using cah.Domain;

namespace cah.Services;

public class CardService : ICardService
{
    public void LoadCards(string path)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CardSet> GetCardSets()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Card> GetCardsFromSets(IEnumerable<CardSet> cardSets)
    {
        throw new NotImplementedException();
    }
}