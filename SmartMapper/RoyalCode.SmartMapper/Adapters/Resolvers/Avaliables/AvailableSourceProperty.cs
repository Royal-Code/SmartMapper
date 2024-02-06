using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Resolutions;
using RoyalCode.SmartMapper.Core.Resolutions;
using System.Text;

namespace RoyalCode.SmartMapper.Adapters.Resolvers.Avaliables;

/// <summary>
/// Property that is available for mapping.
/// </summary>
public class AvailableSourceProperty
{
    /// <summary>
    /// Creates new instance of <see cref="AvailableSourceProperty"/>.
    /// </summary>
    /// <param name="sourceItem">The source item.</param>
    /// <param name="parentySourceProperty">The parent source property, if available.</param>
    public AvailableSourceProperty(SourceItem sourceItem, AvailableSourceProperty? parentySourceProperty = null)
    {
        SourceItem = sourceItem;
        Parent = parentySourceProperty;

        parentySourceProperty?.AddInnerProperty(this);
    }

    /// <summary>
    /// The parent source property, if available.
    /// </summary>
    public AvailableSourceProperty? Parent { get; }

    /// <summary>
    /// if the property is resolved.
    /// </summary>
    public bool Resolved { get; private set; }

    /// <summary>
    /// The source item.
    /// </summary>
    public SourceItem SourceItem { get; }

    /// <summary>
    /// The property options.
    /// </summary>
    public PropertyOptions Options => SourceItem.Options;

    /// <summary>
    /// The resolution options, if available.
    /// </summary>
    public ResolutionOptionsBase? ResolutionOptions => SourceItem.Options.ResolutionOptions;

    /// <summary>
    /// The inner properties, if available.
    /// </summary>
    public AvaliableInnerSourceProperties? InnerProperties { get; private set; }

    /// <summary>
    /// The resolution of the property, if resolved.
    /// </summary>
    public ResolutionBase? Resolution { get; private set; }

    /// <summary>
    /// Assign the resolution of the property.
    /// </summary>
    /// <param name="resolution"></param>
    public void ResolvedBy(ResolutionBase resolution)
    {
        Resolution = resolution;
        Resolved = true;

        Parent?.ChildResolved();
    }

    /// <summary>
    /// Generate a failure message for not resolved source property.
    /// </summary>
    /// <returns></returns>
    public string GetFailureMessage()
    {
        StringBuilder sb = new();
        sb.AppendLine($"The property '{GetPropertyPathString()}' is not resolved.");

        if (InnerProperties is not null)
            sb.Append(InnerProperties.GetFailureMessage());

        return sb.ToString();
    }

    internal object GetPropertyPathString()
    {
        var prefix = Parent?.GetPropertyPathString();
        return prefix is null ? Options.Property.Name : $"{prefix}.{Options.Property.Name}";
    }

    private void AddInnerProperty(AvailableSourceProperty child)
    {
        InnerProperties ??= new AvaliableInnerSourceProperties(this);
        InnerProperties.Add(child);
    }

    private void ChildResolved()
    {
        if (InnerProperties is null)
            throw new InvalidOperationException("The inner properties should be available.");

        var isAllChildrenResolved = InnerProperties.Resolved;
        if (isAllChildrenResolved)
        {
            Resolved = true;
            Parent?.ChildResolved();
        }
    }
}