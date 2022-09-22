using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;
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

    public AdapterResolution Resolve(AdapterResolverContext context)
    {
        throw new NotImplementedException();
    }

    public bool TryResolve(AdapterResolverContext context, [NotNullWhen(true)] out AdapterResolution resolution)
    {
        throw new NotImplementedException();
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
        var context = new AdapterResolverContext(key, null);
        
        
        throw new NotImplementedException();
    }
}