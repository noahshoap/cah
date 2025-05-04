using cah.Abstractions;
using cah.Domain;

namespace cah.GameModes;

public class DemocracyGameMode(Guid gameId, IEnumerable<Card> cards) : AbstractGameMode(gameId, cards)
{
    
}