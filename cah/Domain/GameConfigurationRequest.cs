namespace cah.Domain;

public class GameConfigurationRequest
{
    public string GameMode { get; set; } = "base";
    public IList<Card> Cards { get; set; } = new List<Card>();
}