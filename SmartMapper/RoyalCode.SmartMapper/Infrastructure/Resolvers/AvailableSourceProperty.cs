using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers;

public class AvailableSourceProperty : ResolvableMember
{
    public AvailableSourceProperty(SourceProperty sourceProperty, InnerSourcePropertiesGroup? group = null)
    {
        SourceProperty = sourceProperty;
        Group = group;
        group?.Add(this);
    }

    public SourceProperty SourceProperty { get; }

    public InnerSourcePropertiesGroup? Group { get; }

    public ParameterResolution? Resolution { get; private set; }

    public void ResolvedBy(ParameterResolution resolution)
    {
        Resolution = resolution;
        Resolved = true;
    }
}