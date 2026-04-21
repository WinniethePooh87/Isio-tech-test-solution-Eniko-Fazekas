using GildedRoseKata;
using GildedRoseKata.Strategies.Implementations;
using Xunit;

namespace GildedRoseTests.StrategyTests;

public class ConjuredItemStrategyTests
{
    private readonly ConjuredItemStrategy _strategy;

    public ConjuredItemStrategyTests()
    {
        _strategy = new ConjuredItemStrategy();
    }

    [Theory]
    [InlineData("Conjured Mana Cake", true)]
    [InlineData("Regular Item", false)]
    [InlineData("Aged Brie", false)]
    public void IsMatch_ReturnsExpectedResult(string itemName, bool expectedResult)
    {
        var result = _strategy.IsMatch(itemName);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void Order_Returns3()
    {
        Assert.Equal(3, _strategy.Order);
    }

    [Theory]
    [InlineData(5, 10, 8, 4)]    // Before expiration: quality decreases by 2
    [InlineData(1, 10, 8, 0)]    // Last day before expiration: quality decreases by 2
    [InlineData(0, 10, 6, -1)]   // Expiration day: quality decreases by 4
    [InlineData(-1, 10, 6, -2)]  // After expiration: quality decreases by 4
    public void Update_DecreasesQualityBasedOnSellIn(int initialSellIn, int initialQuality, int expectedQuality, int expectedSellIn)
    {
        var item = new Item { Name = "Conjured Mana Cake", SellIn = initialSellIn, Quality = initialQuality };

        _strategy.Update(item);

        Assert.Equal(expectedQuality, item.Quality);
        Assert.Equal(expectedSellIn, item.SellIn);
    }

    [Theory]
    [InlineData(5, 1, 0, 4)]   // Quality at 1 before expiration: stops at 0 (would decrease by 2)
    [InlineData(0, 3, 0, -1)]  // Quality at 3 when expired: stops at 0 (would decrease by 4)
    [InlineData(5, 0, 0, 4)]   // Quality already at 0: stays at 0
    public void Update_QualityNeverGoesNegative(int initialSellIn, int initialQuality, int expectedQuality, int expectedSellIn)
    {
        var item = new Item { Name = "Conjured Mana Cake", SellIn = initialSellIn, Quality = initialQuality };

        _strategy.Update(item);

        Assert.Equal(expectedQuality, item.Quality);
        Assert.Equal(expectedSellIn, item.SellIn);
    }

    [Fact]
    public void Update_DecreasesSellInByOne()
    {
        var item = new Item { Name = "Conjured Mana Cake", SellIn = 10, Quality = 20 };

        _strategy.Update(item);

        Assert.Equal(9, item.SellIn);
    }
}
