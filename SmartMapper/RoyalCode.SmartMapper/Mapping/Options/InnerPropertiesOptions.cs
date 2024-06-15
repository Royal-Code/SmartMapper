using System.Reflection;
using RoyalCode.SmartMapper.Mapping.Options.Resolutions;

namespace RoyalCode.SmartMapper.Mapping.Options;

/// <summary>
/// Options that contains internal properties of a mapped source property.
/// </summary>
public sealed class InnerPropertiesOptions
{
    private readonly ICollection<ResolutionOptionsBase> innerPropertiesResolutions = [];

    /// <summary>
    /// Creates a new <see cref="InnerPropertiesOptions"/>.
    /// </summary>
    /// <param name="category">mapping category.</param>
    /// <param name="property">The property info.</param>
    public InnerPropertiesOptions(PropertyInfo property)
    {
        InnerSourceOptions = new SourceOptions(MappingCategory.Inner, property.PropertyType);
    }

    /// <summary>
    /// The source options for the inner properties.
    /// </summary>
    public SourceOptions InnerSourceOptions { get; }

    /// <summary>
    /// Adds a resolution for an inner property.
    /// </summary>
    /// <param name="resolution"></param>
    public void AddInnerPropertyResolution(ResolutionOptionsBase resolution)
    {
        if (innerPropertiesResolutions.Contains(resolution))
            return;

        innerPropertiesResolutions.Add(resolution);
    }

    /// <summary>
    /// Get the inner properties resolutions.
    /// </summary>
    /// <returns>
    ///     An enumerable of <see cref="ResolutionOptionsBase"/>.
    /// </returns>
    public IEnumerable<ResolutionOptionsBase> GetInnerPropertiesResolutions()
    {
        return innerPropertiesResolutions;
    }
}
