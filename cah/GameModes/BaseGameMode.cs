using cah.Abstractions;
using cah.Domain;

namespace cah.GameModes;

public class BaseGameMode(Guid gameId, IEnumerable<Card> cards) : AbstractGameMode(gameId, cards)
{
}