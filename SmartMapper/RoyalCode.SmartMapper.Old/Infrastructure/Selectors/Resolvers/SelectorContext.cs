using RoyalCode.SmartMapper.Infrastructure.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Core;
using RoyalCode.SmartMapper.Infrastructure.Selectors.Options;

namespace RoyalCode.SmartMapper.Infrastructure.Selectors.Resolvers;

public class SelectorContext
{
    public MapKey Key { get; }

    public SelectorOptions Options => Configuration.Mappings.SelectorsOptions.GetOptions(Key);

    public ResolutionConfiguration Configuration { get; }
    
    public SelectorContext(
        MapKey key,
        ResolutionConfiguration configuration)
    {
        Key = key;
        Configuration = configuration;
    }
}