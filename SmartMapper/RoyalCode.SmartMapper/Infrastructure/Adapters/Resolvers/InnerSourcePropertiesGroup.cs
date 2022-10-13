namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

public class InnerSourcePropertiesGroup
{
    private readonly List<AvailableSourceProperty> properties = new();

    public InnerSourcePropertiesGroup(SourceProperty sourceProperty)
    {
        SourceProperty = sourceProperty;
    }

    public SourceProperty SourceProperty { get; }
    
    public bool IsResolved => properties.All(p => p.IsResolved);
    
    public void Add(AvailableSourceProperty property) => properties.Add(property);
}