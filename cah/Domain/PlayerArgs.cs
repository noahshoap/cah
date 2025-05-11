using cah.Abstractions;

namespace cah.Domain;

public class PlayerArgs : EventArgs
{
    public IPlayer Player { get; }
    public PlayerArgs(IPlayer player)
    {
        Player = player;
    }
}

public class PlayerPlayedCardArgs : PlayerArgs
{
    public Card Card { get; }

    public PlayerPlayedCardArgs(IPlayer player, Card card) : base(player)
    {
        Card = card;
    }
}