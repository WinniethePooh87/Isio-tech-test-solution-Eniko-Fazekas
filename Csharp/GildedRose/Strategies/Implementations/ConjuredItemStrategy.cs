using GildedRoseKata.Strategies.Interfaces;
using System;
using static GildedRoseKata.ItemConstants;

namespace GildedRoseKata.Strategies.Implementations;

public class ConjuredItemStrategy : IItemStrategy
{
    private const int DegradeMultiplier = 2;

    public int Order => 3;

    public bool IsMatch(string itemName) => itemName == "Conjured Mana Cake";
    public void Update(Item item)
    {
        var decrease = item.SellIn <= SellIn.Expired
            ? Quality.DefaultIncrease * DegradeMultiplier * Quality.ExpiredMultiplier
            : Quality.DefaultIncrease * DegradeMultiplier;

        item.Quality = Math.Max(Quality.Min, item.Quality - decrease);

        item.SellIn -= SellIn.Decrease;
    }
}