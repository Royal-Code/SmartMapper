using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

public class AdapterContext
{
    public MapKey Key { get; }

    public AdapterOptions Options => ResolutionConfiguration.MappingConfiguration.AdaptersOptions.GetOptions(Key);

    public ResolutionConfiguration ResolutionConfiguration { get; }
    
    public AdapterContext(
        MapKey key,
        ResolutionConfiguration resolutionConfiguration)
    {
        Key = key;
        ResolutionConfiguration = resolutionConfiguration;
    }
}