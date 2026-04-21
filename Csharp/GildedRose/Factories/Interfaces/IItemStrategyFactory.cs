using GildedRoseKata.Strategies.Interfaces;

namespace GildedRoseKata.Factories.Interfaces;

public interface IItemStrategyFactory
{
    IItemStrategy GetStrategy(string itemName);
}
