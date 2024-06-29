using System.Text;
using RoyalCode.SmartMapper.Mapping.Options;
using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

namespace RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

/// <summary>
/// A group of inner properties that are available for mapping, from a parent source property.
/// </summary>
public sealed class AvailableInnerSourceProperties
{
    private readonly List<AvailableSourceProperty> properties = [];

    /// <summary>
    /// Create a new instance of <see cref="AvailableInnerSourceProperties"/>.
    /// </summary>
    /// <param name="parentSourceProperty">The available parent source property.</param>
    public AvailableInnerSourceProperties(AvailableSourceProperty parentSourceProperty)
    {
        ParentSourceProperty = parentSourceProperty;
    }

    /// <summary>
    /// The available parent source property.
    /// </summary>
    public AvailableSourceProperty ParentSourceProperty { get; }

    /// <summary>
    /// The source item of the parent source property.
    /// </summary>
    public SourceProperty SourceItem => ParentSourceProperty.SourceProperty;

    /// <summary>
    /// The property options of the parent source property.
    /// </summary>
    public PropertyOptions Options => SourceItem.Options;

    /// <summary>
    /// Check in all inner properties are resolved.
    /// </summary>
    public bool Resolved => properties.TrueForAll(static p => p.Resolved);

    /// <summary>
    /// Add a new inner property.
    /// </summary>
    /// <param name="property">The available inner property.</param>
    public void Add(AvailableSourceProperty property) => properties.Add(property);

    /// <summary>
    /// Create a message that describes the failure of the resolution, informing the properties that can't be resolved.
    /// </summary>
    /// <returns></returns>
    public string GetFailureMessage()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"The inner property '{ParentSourceProperty.GetPropertyPathString()}' is not resolved.");
        sb.AppendLine("The following properties must be resolved:");
        foreach (var property in properties.Where(p => !p.Resolved))
        {
            sb.AppendLine($"- {property.Options.Property.Name}");
        }

        return sb.ToString();
    }
}
