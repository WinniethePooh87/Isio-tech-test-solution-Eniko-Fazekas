using GildedRoseKata;
using GildedRoseKata.Strategies.Implementations;
using Xunit;

namespace GildedRoseTests.StrategyTests;

public class LegendaryItemStrategyTests
{
    private readonly LegendaryItemStrategy _strategy;

    public LegendaryItemStrategyTests()
    {
        _strategy = new LegendaryItemStrategy();
    }

    [Theory]
    [InlineData("Sulfuras, Hand of Ragnaros", true)]
    [InlineData("Regular Item", false)]
    [InlineData("Aged Brie", false)]
    [InlineData("Sulfuras", false)]
    public void IsMatch_ReturnsExpectedResult(string itemName, bool expectedResult)
    {
        var result = _strategy.IsMatch(itemName);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void Order_Returns4()
    {
        Assert.Equal(4, _strategy.Order);
    }

    [Theory]
    [InlineData(0, 80, 80, 0)]     // SellIn at 0: no change
    [InlineData(10, 80, 80, 10)]   // Positive SellIn: no change
    [InlineData(-1, 80, 80, -1)]   // Negative SellIn: no change
    [InlineData(5, 100, 100, 5)]   // Any quality value: no change
    public void Update_NeverChangesQualityOrSellIn(int initialSellIn, int initialQuality, int expectedQuality, int expectedSellIn)
    {
        var item = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = initialSellIn, Quality = initialQuality };

        _strategy.Update(item);

        Assert.Equal(expectedQuality, item.Quality);
        Assert.Equal(expectedSellIn, item.SellIn);
    }
}
