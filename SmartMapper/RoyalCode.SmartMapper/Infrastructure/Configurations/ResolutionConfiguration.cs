using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Configurations;

public class ResolutionConfiguration
{
    private readonly Dictionary<Type, object> resolvers;
    private readonly Dictionary<Type, object> discoveries;

    public ResolutionConfiguration(
        MappingConfiguration mappings,
        Converters converters,
        NameHandlers nameHandlers,
        Dictionary<Type, object> resolvers,
        Dictionary<Type, object> discoveries)
    {
        this.resolvers = resolvers;
        this.discoveries = discoveries;
        
        Mappings = mappings;
        Converters = converters;
        NameHandlers = nameHandlers;
        
        Cache = new();
    }

    public MappingConfiguration Mappings { get; }
    
    public ResolutionCache Cache { get; }
    
    public Converters Converters { get; }

    public NameHandlers NameHandlers { get; }
    
    public T GetResolver<T>()
    {
        if (resolvers.TryGetValue(typeof(T), out var obj))
            return (T)obj;

        throw new ResolverNotFoundException(typeof(T));
    }

    public T GetDiscovery<T>()
    {
        if (discoveries.TryGetValue(typeof(T), out var obj))
            return (T)obj;

        throw new DiscoveryNotFoundException(typeof(T));
    }
}