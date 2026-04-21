using GildedRoseKata.Strategies.Interfaces;
using System;
using static GildedRoseKata.ItemConstants;

namespace GildedRoseKata.Strategies.Implementations;

public class RegularItemStrategy : IItemStrategy
{
    public int Order => int.MaxValue;
    public bool IsMatch(string itemName) => true;
    public void Update(Item item)
    {
        var decrease = item.SellIn <= SellIn.Expired
            ? Quality.DefaultDecrease * Quality.ExpiredMultiplier
            : Quality.DefaultDecrease;

        item.Quality = Math.Max(Quality.Min, item.Quality - decrease);
        item.SellIn -= SellIn.Decrease;
    }
}