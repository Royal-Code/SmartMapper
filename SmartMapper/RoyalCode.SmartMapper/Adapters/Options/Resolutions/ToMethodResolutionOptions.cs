using RoyalCode.SmartMapper.Adapters.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Options.Resolutions;

/// <summary>
/// Options that contains the resolution of a property to be mapped to target method.
/// </summary>
public sealed class ToMethodResolutionOptions : InnerPropertiesResolutionOptionsBase
{
    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="ToMethodResolutionOptions"/>.
    /// </para>
    /// <para>
    ///     Call <see cref="PropertyOptions.ResolvedBy(ResolutionOptionsBase)"/> to add the current instance
    ///     as a resolution of the source property.
    /// </para>
    /// </summary>
    /// <param name="methodOptions"></param>
    /// <param name="resolvedProperty"></param>
    public ToMethodResolutionOptions(MethodOptions methodOptions, PropertyOptions resolvedProperty) : base(resolvedProperty)
    {
        Status = ResolutionStatus.MappedToMethod;
        MethodOptions = methodOptions;
    }

    /// <summary>
    /// The method options mapped by the property.
    /// </summary>
    public MethodOptions MethodOptions { get; }
}
