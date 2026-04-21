using GildedRoseKata.Factories.Interfaces;
using System;
using System.Collections.Generic;
using static GildedRoseKata.ItemConstants;

namespace GildedRoseKata;

public class GildedRose
{
    private readonly IItemStrategyFactory _itemStrategyFactory;
    public IList<Item> Items { get; set; } = new List<Item>();

    public GildedRose(IItemStrategyFactory itemStrategyFactory)
    {
        _itemStrategyFactory = itemStrategyFactory;
    }

    public void UpdateQuality()
    {
        foreach (var item in Items)
        {
            item.Quality = Math.Min(Quality.Max, item.Quality); //quality should never be more than 40
            var strategy = _itemStrategyFactory.GetStrategy(item.Name);
            strategy.Update(item);
        }
    }
}