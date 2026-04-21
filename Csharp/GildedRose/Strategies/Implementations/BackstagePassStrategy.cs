using GildedRoseKata.Strategies.Interfaces;
using System;
using static GildedRoseKata.ItemConstants;

namespace GildedRoseKata.Strategies.Implementations;

public class BackstagePassStrategy : IItemStrategy
{
    private const int ClosingThreshold = 2;
    private const int ApproachingThreshold = 7;
    private const int ApproachingIncrease = 3;
    private const int ClosingIncrease = 4;

    public int Order => 2;

    public bool IsMatch(string itemName) => itemName == "Backstage passes to a TAFKAL80ETC concert";

    public void Update(Item item)
    {
        if (item.SellIn <= SellIn.Expired)
        {
            item.Quality = Quality.Min;
        }
        else
        {
            item.Quality = Math.Min(Quality.Max, item.Quality + GetQualityIncrease(item.SellIn));
        }

        item.SellIn -= SellIn.Decrease;
    }

    private static int GetQualityIncrease(int sellIn)
    {
        if (sellIn <= ApproachingThreshold && sellIn > ClosingThreshold)
            return ApproachingIncrease;

        if (sellIn <= ClosingThreshold)
            return ClosingIncrease;

        return Quality.DefaultIncrease;
    }
}