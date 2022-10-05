
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Configurations;

public class ResolutionConfiguration
{
    public MappingConfiguration Mappings { get; }
    
    public ResolutionCache Cache { get; }
    
    public Converters Converters { get; }
    
    public T GetResolver<T>()
    {
        throw new NotImplementedException();
    }

    public T GetDiscovery<T>()
    {
        throw new NotImplementedException();
    }
}