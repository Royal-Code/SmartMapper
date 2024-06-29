using System.Diagnostics.CodeAnalysis;
using System.Text;
using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Options;
using RoyalCode.SmartMapper.Mapping.Options.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

namespace RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

/// <summary>
/// Property that is available for mapping.
/// </summary>
public sealed class AvailableSourceProperty
{
    /// <summary>
    /// Creates new instance of <see cref="AvailableSourceProperty"/>.
    /// </summary>
    /// <param name="sourceProperty">The source item.</param>
    /// <param name="parentSourceProperty">The parent source property, if available.</param>
    public AvailableSourceProperty(SourceProperty sourceProperty, AvailableSourceProperty? parentSourceProperty = null)
    {
        SourceProperty = sourceProperty;
        Parent = parentSourceProperty;

        parentSourceProperty?.AddInnerProperty(this);
    }

    /// <summary>
    /// The parent source property, if available.
    /// </summary>
    public AvailableSourceProperty? Parent { get; }

    /// <summary>
    /// if the property is resolved.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Resolution))]
    public bool Resolved => Resolution is not null;

    /// <summary>
    /// The source item.
    /// </summary>
    public SourceProperty SourceProperty { get; }

    /// <summary>
    /// The property options.
    /// </summary>
    public PropertyOptions Options => SourceProperty.Options;

    /// <summary>
    /// The resolution options, if available.
    /// </summary>
    public ResolutionOptionsBase? ResolutionOptions => SourceProperty.Options.ResolutionOptions;

    /// <summary>
    /// The inner properties, if available.
    /// </summary>
    public AvailableInnerSourceProperties? InnerProperties { get; private set; }

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

        Parent?.ChildResolved();
    }

    /// <summary>
    /// Complete the resolution of the property, marking the source item as resolved.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     Case the property is not resolved.
    /// </exception>
    public void Completed()
    {
        if (!Resolved)
            throw new InvalidOperationException("The property is not resolved.");
        
        SourceProperty.ResolvedBy(Resolution);
        Parent?.Completed();
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
        InnerProperties ??= new AvailableInnerSourceProperties(this);
        InnerProperties.Add(child);
    }

    private void ChildResolved()
    {
        if (InnerProperties is null)
            throw new InvalidOperationException("The inner properties should be available.");

        var isAllChildrenResolved = InnerProperties.Resolved;
        if (isAllChildrenResolved)
        {
            ResolvedBy(new InnerPropertiesResolution(SourceProperty, InnerProperties));
        }
    }
}