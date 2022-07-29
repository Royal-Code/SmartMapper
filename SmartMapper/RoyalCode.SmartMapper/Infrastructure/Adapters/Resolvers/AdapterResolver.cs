using RoyalCode.SmartMapper.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Core;
using RoyalCode.SmartMapper.Resolutions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

/// <summary>
/// <para>
///     Responsible for resolving the adapter type mapping between the source type and the destination type.
/// </para>
/// <para>
///     Once solved, the result is stored internally.
/// </para>
/// </summary>
public class AdapterResolver
{
    private readonly Dictionary<MapKey, object> resolutions = new();
    private readonly MappingConfiguration mappingConfiguration;
    
    public AdapterResolver(MappingConfiguration mappingConfiguration)
    {
        this.mappingConfiguration = mappingConfiguration;
    }
    
    public IAdapterResolution<TSource, TTarget> GetResolution<TSource, TTarget>()
    {
        var key = new MapKey(typeof(TSource), typeof(TTarget));
        
        if (resolutions.TryGetValue(key, out var resolution))
            return (IAdapterResolution<TSource, TTarget>) resolution;
        
        resolution = CreateResolution(key);
        resolutions.Add(key, resolution);
        
        return (IAdapterResolution<TSource, TTarget>)resolution;
    }

    private object CreateResolution(MapKey key)
    {
        var options = mappingConfiguration.AdaptersOptions.GetOptions(key);
        var context = new ResolverContext(key, options, mappingConfiguration);
        
        
        throw new NotImplementedException();
    }
}

public class ResolverContext
{
    public MapKey Key { get; }
    
    public AdapterOptions Options { get; }
    
    public MappingConfiguration MappingConfiguration { get; }
    
    public ResolverContext(MapKey key, AdapterOptions options, MappingConfiguration mappingConfiguration)
    {
        Key = key;
        Options = options;
        MappingConfiguration = mappingConfiguration;
    }
}