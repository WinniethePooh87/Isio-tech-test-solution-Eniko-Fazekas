using GildedRoseKata;
using GildedRoseKata.Strategies.Implementations;
using Xunit;

namespace GildedRoseTests.StrategyTests;

public class RegularItemStrategyTests
{
    private readonly RegularItemStrategy _strategy;

    public RegularItemStrategyTests()
    {
        _strategy = new RegularItemStrategy();
    }

    [Theory]
    [InlineData("Any Item", true)]
    [InlineData("Regular Item", true)]
    [InlineData("Literally Anything", true)]
    public void IsMatch_ReturnsExpectedResult(string itemName, bool expectedResult)
    {
        var result = _strategy.IsMatch(itemName);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void Order_ReturnsMaxValue()
    {
        Assert.Equal(int.MaxValue, _strategy.Order);
    }

    [Theory]
    [InlineData(5, 10, 9, 4)]    // Before expiration: quality decreases by 1
    [InlineData(1, 10, 9, 0)]    // Last day before expiration: quality decreases by 1
    [InlineData(0, 10, 8, -1)]   // Expiration day: quality decreases by 2
    [InlineData(-1, 10, 8, -2)]  // After expiration: quality decreases by 2
    public void Update_DecreasesQualityBasedOnSellIn(int initialSellIn, int initialQuality, int expectedQuality, int expectedSellIn)
    {
        var item = new Item { Name = "Regular Item", SellIn = initialSellIn, Quality = initialQuality };

        _strategy.Update(item);

        Assert.Equal(expectedQuality, item.Quality);
        Assert.Equal(expectedSellIn, item.SellIn);
    }

    [Theory]
    [InlineData(5, 0, 0, 4)]   // Quality at 0 before expiration: stays at 0
    [InlineData(0, 1, 0, -1)]  // Quality would go negative when expired: stops at 0
    [InlineData(5, 1, 0, 4)]   // Quality at 1 before expiration: stops at 0
    public void Update_QualityNeverGoesNegative(int initialSellIn, int initialQuality, int expectedQuality, int expectedSellIn)
    {
        var item = new Item { Name = "Regular Item", SellIn = initialSellIn, Quality = initialQuality };

        _strategy.Update(item);

        Assert.Equal(expectedQuality, item.Quality);
        Assert.Equal(expectedSellIn, item.SellIn);
    }

    [Fact]
    public void Update_DecreasesSellInByOne()
    {
        var item = new Item { Name = "Regular Item", SellIn = 10, Quality = 20 };

        _strategy.Update(item);

        Assert.Equal(9, item.SellIn);
    }
}
