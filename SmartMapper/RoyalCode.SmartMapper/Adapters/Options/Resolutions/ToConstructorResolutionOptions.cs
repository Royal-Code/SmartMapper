using RoyalCode.SmartMapper.Adapters.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Options.Resolutions;

/// <summary>
/// Options that contains the resolution of a property to be mapped to a constructor.
/// </summary>
public sealed class ToConstructorResolutionOptions : InnerPropertiesResolutionOptionsBase
{
    private readonly ICollection<ResolutionOptionsBase> innerPropertiesResolutions = [];

    /// <summary>
    /// Creates a new instance of <see cref="ToConstructorResolutionOptions"/>.
    /// </summary>
    /// <param name="resolvedProperty">A resolved property.</param>
    public ToConstructorResolutionOptions(PropertyOptions resolvedProperty) 
        : base(resolvedProperty)
    {
        Status = ResolutionStatus.MappedToConstructor;
    }

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
