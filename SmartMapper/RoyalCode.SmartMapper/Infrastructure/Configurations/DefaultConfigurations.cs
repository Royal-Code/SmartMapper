using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

namespace RoyalCode.SmartMapper.Infrastructure.Configurations;

public static class DefaultConfigurations
{
    public static ConfigurationBuilder Default(this ConfigurationBuilder builder)
    {
        builder.AddResolver(new AdapterResolver());
        builder.AddResolver(new ActivationResolver());
        builder.AddResolver(new ConstructorResolver());
        builder.AddResolver(new ConstructorParameterResolver());

        return builder;
    }
}