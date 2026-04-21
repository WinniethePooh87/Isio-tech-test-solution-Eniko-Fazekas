using GildedRoseKata;
using GildedRoseKata.Strategies.Implementations;
using Xunit;

namespace GildedRoseTests.StrategyTests;

public class BackstagePassStrategyTests
{
    private readonly BackstagePassStrategy _strategy;

    public BackstagePassStrategyTests()
    {
        _strategy = new BackstagePassStrategy();
    }

    [Theory]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", true)]
    [InlineData("Regular Item", false)]
    [InlineData("Backstage pass", false)]
    public void IsMatch_ReturnsExpectedResult(string itemName, bool expectedResult)
    {
        var result = _strategy.IsMatch(itemName);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void Order_Returns2()
    {
        Assert.Equal(2, _strategy.Order);
    }

    [Theory]
    [InlineData(15, 20, 21, 14)]  // SellIn > 10: quality increases by 1
    [InlineData(7, 20, 23, 6)]    // 3 ≤ SellIn ≤ 10: quality increases by 3
    [InlineData(2, 20, 24, 1)]    // SellIn = 2: quality increases by 4
    [InlineData(1, 20, 24, 0)]    // SellIn = 1: quality increases by 4
    [InlineData(0, 20, 0, -1)]    // SellIn = 0: concert passed, quality drops to 0
    public void Update_IncreasesQualityBasedOnSellInThreshold(int initialSellIn, int initialQuality, int expectedQuality, int expectedSellIn)
    {
        var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = initialSellIn, Quality = initialQuality };

        _strategy.Update(item);

        Assert.Equal(expectedQuality, item.Quality);
        Assert.Equal(expectedSellIn, item.SellIn);
    }

    [Fact]
    public void Update_QualityIsZeroWhenSellInDatePassed()
    {
        var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 40 };

        _strategy.Update(item);

        Assert.Equal(0, item.Quality);
        Assert.Equal(-1, item.SellIn);
    }

    [Fact]
    public void Update_QualityNeverExceedsMaximum()
    {
        var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 40 };

        _strategy.Update(item);

        Assert.Equal(40, item.Quality);
        Assert.Equal(14, item.SellIn);
    }

    [Fact]
    public void Update_DecreasesSellInByOne()
    {
        var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 20 };

        _strategy.Update(item);

        Assert.Equal(9, item.SellIn);
    }
}