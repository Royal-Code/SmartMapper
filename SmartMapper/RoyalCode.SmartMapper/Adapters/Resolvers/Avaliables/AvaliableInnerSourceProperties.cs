using RoyalCode.SmartMapper.Adapters.Options;
using System.Text;

namespace RoyalCode.SmartMapper.Adapters.Resolvers.Avaliables;

public class AvaliableInnerSourceProperties
{
    private readonly List<AvailableSourceProperty> properties = [];

    public AvaliableInnerSourceProperties(PropertyOptions options)
    {
        Options = options;
    }

    public PropertyOptions Options { get; }

    public bool Resolved => properties.All(p => p.Resolved);

    public void Add(AvailableSourceProperty property) => properties.Add(property);

    public string GetFailureMessage()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"The inner property '{Options..GetPropertyPathString()}' is not resolved.");
        sb.AppendLine("The following properties must be resolved:");
        foreach (var property in properties.Where(p => !p.Resolved))
        {
            sb.AppendLine($"- {property.SourceProperty.MemberInfo.Name}");
        }

        return sb.ToString();
    }
}
