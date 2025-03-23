using cah.Domain;

namespace cah.Abstractions;

public interface IGameFactory
{
    public IGame CreateGame(GameConfigurationRequest configuration);
}