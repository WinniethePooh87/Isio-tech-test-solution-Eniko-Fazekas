using GildedRoseKata.Factories.Interfaces;
using GildedRoseKata.Strategies.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GildedRoseKata.Factories.Implementations;

public class ItemStrategyFactory : IItemStrategyFactory
{
    private readonly IEnumerable<IItemStrategy> _strategies;

    public ItemStrategyFactory(IEnumerable<IItemStrategy> strategies)
    {
        _strategies = strategies;
    }

    public IItemStrategy GetStrategy(string itemName) =>
        _strategies
            .OrderBy(s => s.Order)
            .First(s => s.IsMatch(itemName));
}
