using GildedRoseKata;
using GildedRoseKata.Factories.Implementations;
using GildedRoseKata.Strategies.Implementations;
using GildedRoseKata.Strategies.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace GildedRoseTests;

public class GildedRoseTests
{
    private readonly GildedRose _app;

    public GildedRoseTests()
    {
        var strategies = new List<IItemStrategy>
        {
            new AgedItemStrategy(),
            new BackstagePassStrategy(),
            new ConjuredItemStrategy(),
            new LegendaryItemStrategy(),
            new RegularItemStrategy()
        };

        var factory = new ItemStrategyFactory(strategies);
        _app = new GildedRose(factory);
    }

    [Theory]
    [InlineData("Regular Item", 5, 10, 9, 4)]
    [InlineData("Regular Item", 0, 10, 8, -1)]
    [InlineData("Aged Brie", 5, 10, 11, 4)]
    [InlineData("Sulfuras, Hand of Ragnaros", 5, 80, 40, 5)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 15, 20, 21, 14)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 10, 20, 21, 9)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 7, 20, 23, 6)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 5, 20, 23, 4)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 1, 20, 24, 0)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 0, 20, 0, -1)]
    [InlineData("Conjured Mana Cake", 5, 10, 8, 4)]
    [InlineData("Conjured Mana Cake", 0, 10, 6, -1)]
    public void UpdateQuality_UpdatesItemsCorrectly(string itemName, int initialSellIn, int initialQuality, int expectedQuality, int expectedSellIn)
    {
        _app.Items = new List<Item> { new Item { Name = itemName, SellIn = initialSellIn, Quality = initialQuality } };

        _app.UpdateQuality();

        Assert.Equal(expectedQuality, _app.Items[0].Quality);
        Assert.Equal(expectedSellIn, _app.Items[0].SellIn);
    }

    [Fact]
    public void UpdateQuality_QualityAbove40_IsSetTo40BeforeStrategyApplies()
    {
        _app.Items = new List<Item>
        {
            new Item { Name = "Regular Item", SellIn = 5, Quality = 100 }
        };

        _app.UpdateQuality();

        Assert.Equal(39, _app.Items[0].Quality);
    }
}