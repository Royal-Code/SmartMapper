using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Discovery;

public class ConstructorParameterDiscovery
{
    public ConstructorParameterDiscovered Discover(ConstructorParameterDiscoveryContext context)
    {
        foreach (var property in context.SourceProperties)
        {
            if (property.Options.ResolutionStatus == ResolutionStatus.Undefined)
            {

            }
            else if (property.Options.ResolutionStatus == ResolutionStatus.MappedToConstructor)
            {

            }
            else if (property.Options.ResolutionStatus == ResolutionStatus.MappedToConstructorParameter)
            {

            }
        }

        return new ConstructorParameterDiscovered();
    }
}

public record ConstructorParameterDiscoveryContext(
    IEnumerable<SourceProperty> SourceProperties,
    IEnumerable<TargetParameter> Parameters,
    ResolutionConfiguration Configuration);
    
public record ConstructorParameterDiscovered();