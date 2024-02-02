using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Adapters;

namespace RoyalCode.SmartMapper.Infrastructure.Core;

public class ResolutionCache
{
    private readonly Dictionary<MapKey, AdapterResolution> adapters = new();

    public bool TryGetAdapter(MapKey key, [NotNullWhen(true)] out AdapterResolution? resolution)
    {
        resolution = null;
        lock (adapters)
        {
            if (adapters.ContainsKey(key))
                resolution = adapters[key];
        }

        return resolution != null;
    }

    public void Store(MapKey key, AdapterResolution resolution)
    {
        lock (adapters)
        {
            adapters[key] = resolution;
        }
    }
}