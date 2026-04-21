using GildedRoseKata.Strategies.Interfaces;

namespace GildedRoseKata.Strategies.Implementations;

public class LegendaryItemStrategy : IItemStrategy
{
    public int Order => 4;
    public bool IsMatch(string itemName) => itemName == "Sulfuras, Hand of Ragnaros";

    public void Update(Item item)
    {
        //Legendary items never change
    }
}