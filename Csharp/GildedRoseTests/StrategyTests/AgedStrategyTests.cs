using GildedRoseKata;
using GildedRoseKata.Strategies.Implementations;
using Xunit;

namespace GildedRoseTests.StrategyTests;

public class AgedStrategyTests
{
    private readonly AgedItemStrategy _strategy;

    public AgedStrategyTests()
    {
        _strategy = new AgedItemStrategy();
    }

    [Theory]
    [InlineData("Aged Brie", true)]
    [InlineData("Regular Item", false)]
    [InlineData("Aged Wine", false)]
    public void IsMatch_ReturnsExpectedResult(string itemName, bool expectedResult)
    {
        var result = _strategy.IsMatch(itemName);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void Order_Returns1()
    {
        Assert.Equal(1, _strategy.Order);
    }

    [Fact]
    public void Update_IncreasesQualityByOne()
    {
        var item = new Item { Name = "Aged Brie", SellIn = 5, Quality = 10 };

        _strategy.Update(item);

        Assert.Equal(11, item.Quality);
        Assert.Equal(4, item.SellIn);
    }

    [Fact]
    public void Update_QualityNeverExceedsMaximum()
    {
        var item = new Item { Name = "Aged Brie", SellIn = 5, Quality = 40 };

        _strategy.Update(item);

        Assert.Equal(40, item.Quality);
        Assert.Equal(4, item.SellIn);
    }

    [Fact]
    public void Update_IncreasesQuality_WhenSellInIsZero()
    {
        var item = new Item { Name = "Aged Brie", SellIn = 0, Quality = 10 };

        _strategy.Update(item);

        Assert.Equal(11, item.Quality);
        Assert.Equal(-1, item.SellIn);
    }

    [Fact]
    public void Update_IncreasesQuality_WhenSellInIsNegative()
    {
        var item = new Item { Name = "Aged Brie", SellIn = -1, Quality = 10 };

        _strategy.Update(item);

        Assert.Equal(11, item.Quality);
        Assert.Equal(-2, item.SellIn);
    }

    [Fact]
    public void Update_DecreasesSellInByOne()
    {
        var item = new Item { Name = "Aged Brie", SellIn = 10, Quality = 20 };

        _strategy.Update(item);

        Assert.Equal(9, item.SellIn);
    }
}
