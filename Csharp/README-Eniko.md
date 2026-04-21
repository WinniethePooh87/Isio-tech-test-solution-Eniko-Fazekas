# Gilded Rose starting position in C# xUnit

## Problems to solve

- How to categorize items without modifying the `Item` class
- How to eliminate spaghetti code in GildedRose

## Solution

In this part, I'm going to explain all the decisions I have made while working on the solution.

I have chosen the Strategy Design Pattern, combining it with the Mactory Method Pattern.
I'm very familiar with these patterns and used them in two of my projects.
I have used the strategy pattern because different items should behave differently, and using 
the strategy pattern helps to encapsulate these behaviors in the right place. Plus, it helps avoid
adding a lot of if-else branches.

I took a look at the code in `UpdateQuality` and compared it with the specification. 
They didn't match 100%, so I relied mostly on the specification. As categories are not given, 
I use item names as identifiers to map items to strategies due to the lack of explicit type information. 
(I have decided to do this because in the `items` list in `Program.cs`, there are items with the same name - an 
`ItemType` as an identifier would be more ideal though.)

Based on `UpdateQuality` and the `items` list in `Program.cs`, I have created `AgedItemStrategy`, 
`BackstagePassStrategy`, `ConjuredItemStrategy`, `LegendaryItemStrategy`, and `RegularItemStrategy`. 
(`RegularItemStrategy` is for the rest of the items whose names were not used as a condition, and 
I also didn't find any duplication in their names, plus there were no mention to them in the specification as special items).

Each strategy class contains an `Order` property, an `IsMatch`, and an `Update` method. 
I introduced the `Order` because the factory method in `ItemStrategyFactory` loops through 
all implementations of `IItemStrategy`, and I had to make sure `RegularItemStrategy` would run last, as I didn't
want to add a list of names to it to avoid having to always maintain the list in the future. 

I had two options to consider (I already ruled out the option to add a name list to it). 
The first one is to add an order to the strategies, and the second is to explicitly register all 
strategies in a certain order in the DI class—registering `RegularItemStrategy` in the last position. 

I chose the first one, as this way, every time a new strategy is added, strategies that implement `IItemStrategy`
will not need to be maintained in any way in the DI class. Plus, having an order explicitly added is more telling
for developers who work on it in the future.

I added the different functionalities to each strategy in the `Update` method, following the instructions in the specification.

I have created a constant class `ItemConstants` for constants that are used in different strategies and considered global.
And I introduced some local ones as well, with descriptive names.

I have considered something that was not written in the specification. The specification says that the maximum quantity
is 40, but it didn't say explicitly what to do with the items that were added to the `items` list with a number higher than 40.
So inside `UpdateQuality`, I maximised their value at 40. 

I have added unit tests for each strategy class and updated the `GildedRoseTests`.
 
## NuGet package

The only package that I added is Autofac for dependency injection. It should be automatically restored.