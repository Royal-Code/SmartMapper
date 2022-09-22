using RoyalCode.SmartMapper.Configurations;

namespace RoyalCode.SmartMapper.Infrastructure.Core;

public class ResolutionConfiguration
{
    public MappingConfiguration MappingConfiguration { get; }
    
    public T GetResolver<T>()
    {
        throw new NotImplementedException();
    }
}