using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

public class AdapterContext
{
    public MapKey Key { get; }

    public AdapterOptions Options => Configuration.Mappings.AdaptersOptions.GetOptions(Key);

    public ResolutionConfiguration Configuration { get; }
    
    public AdapterContext(
        MapKey key,
        ResolutionConfiguration configuration)
    {
        Key = key;
        Configuration = configuration;
    }
}