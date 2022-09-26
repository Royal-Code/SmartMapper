
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Configurations;

public class ResolutionConfiguration
{
    public MappingConfiguration Mappings { get; }
    
    public ResolutionCache Cache { get; }
    
    public T GetResolver<T>()
    {
        throw new NotImplementedException();
    }
}