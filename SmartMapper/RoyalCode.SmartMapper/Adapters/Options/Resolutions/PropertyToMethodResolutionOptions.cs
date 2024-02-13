using RoyalCode.SmartMapper.Adapters.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Options.Resolutions;

/// <summary>
/// Resolution option for map a property to a method.
/// </summary>
public class PropertyToMethodResolutionOptions : ResolutionOptionsBase
{
    /// <summary>
    /// Creates a new instance of <see cref="PropertyToMethodResolutionOptions"/>.
    /// </summary>
    /// <param name="resolvedProperty">The resolved property.
    public PropertyToMethodResolutionOptions(PropertyOptions resolvedProperty, MethodOptions methodOptions) 
        : base(resolvedProperty)
    {
        Status = ResolutionStatus.Undefined;
        MethodOptions = methodOptions;
    }

    /// <summary>
    /// The method options.
    /// </summary>
    public MethodOptions MethodOptions { get; }

    /// <summary>
    /// The strategy to map the property to a target method.
    /// </summary>
    public ToMethodStrategy Strategy { get; internal set; }

    internal void MapAsParameter()
    {
        Status = ResolutionStatus.MappedToMethodParameter;
    }

    internal void MapInnerParameters()
    {
        Status = ResolutionStatus.MappedToMethod;
    }
}
