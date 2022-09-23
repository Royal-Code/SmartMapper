
namespace RoyalCode.SmartMapper.Infrastructure.Configurations;

public class ResolutionConfiguration
{
    public MappingConfiguration MappingConfiguration { get; }
    
    public T GetResolver<T>()
    {
        throw new NotImplementedException();
    }
}