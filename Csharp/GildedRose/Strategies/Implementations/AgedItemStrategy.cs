using GildedRoseKata.Strategies.Interfaces;
using System;
using static GildedRoseKata.ItemConstants;

namespace GildedRoseKata.Strategies.Implementations;

public class AgedItemStrategy : IItemStrategy
{
    public int Order => 1;
    public bool IsMatch(string itemName) => itemName == "Aged Brie";

    public void Update(Item item)
    {
        item.Quality = Math.Min(Quality.Max, item.Quality + Quality.DefaultIncrease);
        item.SellIn -= SellIn.Decrease;
    }
}