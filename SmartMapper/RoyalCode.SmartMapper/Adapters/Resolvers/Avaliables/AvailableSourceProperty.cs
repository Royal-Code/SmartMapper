using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Resolutions;
using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Resolvers.Avaliables;

/// <summary>
/// Property that is available for mapping.
/// </summary>
public class AvailableSourceProperty
{
    /// <summary>
    /// Creates new instance of <see cref="AvailableSourceProperty"/>.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="innerProperties"></param>
    public AvailableSourceProperty(PropertyOptions options, AvaliableInnerSourceProperties? innerProperties = null)
    {
        Options = options;
        InnerProperties = innerProperties;
        InnerProperties?.Add(this);
    }

    /// <summary>
    /// if the property is resolved.
    /// </summary>
    public bool Resolved { get; private set; }

    /// <summary>
    /// The property options.
    /// </summary>
    public PropertyOptions Options { get; }

    /// <summary>
    /// The inner properties, if available.
    /// </summary>
    public AvaliableInnerSourceProperties? InnerProperties { get; }

    /// <summary>
    /// The resolution of the property, if resolved.
    /// </summary>
    public ResolutionBase? Resolution { get; private set; }

    /// <summary>
    /// Assign the resolution of the property.
    /// </summary>
    /// <param name="resolution"></param>
    public void ResolvedBy(ParameterResolution resolution)
    {
        Resolution = resolution;
        Resolved = true;
    }
}