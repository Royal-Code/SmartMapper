using RoyalCode.SmartMapper.Adapters.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Options.Resolutions;

/// <summary>
/// Options that contains the resolution of a property to be mapped to a constructor.
/// </summary>
public sealed class ToConstructorResolutionOptions : InnerPropertiesResolutionOptionsBase
{
    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="ToConstructorResolutionOptions"/>.
    /// </para>
    /// <para>
    ///     Call <see cref="PropertyOptions.ResolvedBy(ResolutionOptionsBase)"/> to add the current instance
    ///     as a resolution of the source property.
    /// </para>
    /// </summary>
    /// <param name="resolvedProperty">A resolved property.</param>
    public ToConstructorResolutionOptions(PropertyOptions resolvedProperty) 
        : base(resolvedProperty)
    {
        Status = ResolutionStatus.MappedToConstructor;
    }
}
