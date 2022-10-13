namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

public class AvailableSourceProperty
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

    public bool IsResolved { get; private set; }
    
    public void ResolvedBy(TargetParameter targetParameter)
    {
        TargetParameter = targetParameter;
        IsResolved = true;
    }
}