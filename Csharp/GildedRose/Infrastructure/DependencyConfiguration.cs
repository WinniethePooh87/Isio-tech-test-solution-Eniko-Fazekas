using Autofac;
using GildedRoseKata;
using GildedRoseKata.Factories.Implementations;
using GildedRoseKata.Factories.Interfaces;
using GildedRoseKata.Strategies.Interfaces;


public static class DependencyConfiguration
{
    public static IContainer Build()
    {
        var builder = new ContainerBuilder();

        builder.RegisterType<GildedRose>().AsSelf();

        RegisterStrategies(builder);
        RegisterFactories(builder);

        return builder.Build();
    }

    private static void RegisterStrategies(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(IItemStrategy).Assembly)
               .AssignableTo<IItemStrategy>()
               .As<IItemStrategy>();
    }

    private static void RegisterFactories(ContainerBuilder builder)
    {
        builder.RegisterType<ItemStrategyFactory>().As<IItemStrategyFactory>();
    }
}
