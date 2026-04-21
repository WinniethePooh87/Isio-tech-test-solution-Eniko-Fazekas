namespace GildedRoseKata.Strategies.Interfaces
{
    public interface IItemStrategy
    {
        int Order { get; }
        bool IsMatch(string itemName);
        void Update(Item item);
    }
}