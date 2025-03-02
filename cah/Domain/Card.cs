namespace cah.Domain;

public enum CardType
{
    Question,
    Answer,
}

public class Card
{
    public Card(uint id, string text, CardType type, string cardSetName)
    {
        Id = id;
        Text = text;
        Type = type;
        CardSetName = cardSetName;
    }
    
    public uint Id { get; private set; }
    public string Text {get; private set;}
    public CardType Type { get; private set; }
    public string CardSetName {get; private set;}
}