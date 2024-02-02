
namespace RoyalCode.SmartMapper.Adapters.Testings.UseCases;

public static class DIContainer
{
    private static readonly IServiceProvider sp = CreateServiceProvider(); 

    private static IServiceProvider CreateServiceProvider()
    {
        var services = new ServiceCollection();
        services.AddScoped<IAdapter, DefaultAdapter>();

        return services.BuildServiceProvider();
    }

    public static IServiceProvider GetServiceProvider()
    {
        var scope = sp.CreateScope();
        return scope.ServiceProvider;
    }
}
