using cah.Domain;

namespace cah.Abstractions;

public interface ICardSet
{
    public IEnumerable<Card> GetCards();
}