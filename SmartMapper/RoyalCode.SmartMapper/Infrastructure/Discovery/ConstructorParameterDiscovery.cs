using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Core;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Discovery;

public class ConstructorParameterDiscovery
{
    public bool TryGetPropertyForParameter(ConstructorParameterDiscoveryContext context,
        [NotNullWhen(true)] out SourceProperty? sourceProperty)
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

        sourceProperty = null;
        return false;
    }
}

public record ConstructorParameterDiscoveryContext(
    IEnumerable<SourceProperty> SourceProperties,
    ParameterInfo Parameter,
    ResolutionConfiguration Configuration);