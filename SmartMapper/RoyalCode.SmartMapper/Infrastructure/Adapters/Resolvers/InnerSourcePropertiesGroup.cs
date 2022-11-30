using System.Text;

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

    public string GetFailureMessage()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"The inner property '{SourceProperty.GetPropertyPathString()}' is not resolved.");
        sb.AppendLine("The following properties must be resolved:");
        foreach (var property in properties.Where(p => !p.IsResolved))
        {
            sb.AppendLine($"- {property.SourceProperty.PropertyInfo.Name}");
        }

        return sb .ToString();
    }
}