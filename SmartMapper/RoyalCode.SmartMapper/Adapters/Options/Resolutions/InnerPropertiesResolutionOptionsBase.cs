
namespace RoyalCode.SmartMapper.Adapters.Options.Resolutions;

/// <summary>
/// The base class for resolution options that contains inner properties.
/// </summary>
public abstract class InnerPropertiesResolutionOptionsBase : ResolutionOptionsBase
{
    private readonly ICollection<ResolutionOptionsBase> innerPropertiesResolutions = [];

    /// <summary>
    /// <para>
    ///     Base constructor for the resolution options that contains inner properties.
    /// </para>
    /// <para>
    ///     Call <see cref="PropertyOptions.ResolvedBy(ResolutionOptionsBase)"/> to add the current instance
    ///     as a resolution of the source property.
    /// </para>
    /// </summary>
    /// <param name="resolvedProperty">The source property related to the assignment.</param>
    protected InnerPropertiesResolutionOptionsBase(PropertyOptions resolvedProperty) : base(resolvedProperty)
    {
        InnerSourceOptions = new SourceOptions(resolvedProperty.Property.PropertyType);
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
