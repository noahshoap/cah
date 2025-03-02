using cah.Domain;

namespace cah.Abstractions;

public interface ICardService
{
    public void LoadCards(string path);
    public IEnumerable<CardSet> GetCardSets();
    public IEnumerable<Card> GetCardsFromSets(IEnumerable<CardSet> cardSets);
}