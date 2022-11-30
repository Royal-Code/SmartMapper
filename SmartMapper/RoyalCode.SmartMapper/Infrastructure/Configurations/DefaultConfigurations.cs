using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;
using RoyalCode.SmartMapper.Infrastructure.Discovery;

namespace RoyalCode.SmartMapper.Infrastructure.Configurations;

public static class DefaultConfigurations
{
    public static ConfigurationBuilder Default(this ConfigurationBuilder builder)
    {
        builder.AddResolver(new AdapterResolver());
        builder.AddResolver(new ActivationResolver());
        builder.AddResolver(new ConstructorResolver());
        builder.AddResolver(new ConstructorParameterResolver());


        builder.AddDiscovery(new ConstructorParameterDiscovery());
        
        return builder;
    }
}