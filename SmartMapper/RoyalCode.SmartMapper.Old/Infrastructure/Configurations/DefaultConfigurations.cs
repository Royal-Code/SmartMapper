using RoyalCode.SmartMapper.Infrastructure.Discovery;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Activations;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.AssignmentStrategies;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Constructors;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Parameters;

namespace RoyalCode.SmartMapper.Infrastructure.Configurations;

public static class DefaultConfigurations
{
    public static ConfigurationBuilder Default(this ConfigurationBuilder builder)
    {
        builder.AddResolver(new AdapterResolver());
        builder.AddResolver(new ActivationResolver());
        builder.AddResolver(new ConstructorResolver());
        builder.AddResolver(new ConstructorParameterResolver());
        builder.AddResolver(new ParameterResolver());
        builder.AddResolver(new AssignmentStrategyResolver());
        builder.AddManyResolver(GetValueAssignmentResolver());

        builder.AddDiscovery(new ParameterDiscovery());
        
        return builder;
    }

    private static IValueAssignmentResolver[] GetValueAssignmentResolver()
    {
        return new IValueAssignmentResolver[]
        {
            new DirectValueAssignmentResolver(),
            new CastValueAssignmentResolver(),
            new ConvertValueAssignmentResolver(),
            new AdaptValueAssignmentResolver(),
            new ProcessorValueAssignmentResolver(),
            new SelectValueAssignmentResolver(),
        };
    }
}