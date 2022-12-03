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

    public TargetParameter? TargetParameter { get; private set; }

    public void ResolvedBy(TargetParameter targetParameter)
    {
        TargetParameter = targetParameter;
        Resolved = true;
    }
}