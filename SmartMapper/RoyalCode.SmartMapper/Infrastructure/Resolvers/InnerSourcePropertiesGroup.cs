using System.Text;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers;

public class InnerSourcePropertiesGroup
{
    private readonly List<AvailableSourceProperty> properties = new();

    public InnerSourcePropertiesGroup(SourceProperty sourceProperty)
    {
        SourceProperty = sourceProperty;
    }

    public SourceProperty SourceProperty { get; }

    public bool Resolved => properties.All(p => p.Resolved);

    public void Add(AvailableSourceProperty property) => properties.Add(property);

    public string GetFailureMessage()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"The inner property '{SourceProperty.GetPropertyPathString()}' is not resolved.");
        sb.AppendLine("The following properties must be resolved:");
        foreach (var property in properties.Where(p => !p.Resolved))
        {
            sb.AppendLine($"- {property.SourceProperty.MemberInfo.Name}");
        }

        return sb.ToString();
    }
}