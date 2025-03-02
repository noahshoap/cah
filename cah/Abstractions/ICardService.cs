using cah.Domain;

namespace cah.Abstractions;

public interface ICardService
{
    public void LoadCards(string path);
    public IEnumerable<ICardSet> GetCardSets();
    public IEnumerable<Card> GetCardsFromSets(IEnumerable<ICardSet> cardSets);
}