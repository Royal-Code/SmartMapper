using RoyalCode.SmartMapper.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

public class AdapterResolverContext
{
    public MapKey Key { get; }

    public AdapterOptions Options => ResolutionConfiguration.MappingConfiguration.AdaptersOptions.GetOptions(Key);

    public ResolutionConfiguration ResolutionConfiguration { get; }
    
    public AdapterResolverContext(
        MapKey key,
        ResolutionConfiguration resolutionConfiguration)
    {
        Key = key;
        ResolutionConfiguration = resolutionConfiguration;
    }
}