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
    /// <param name="resolvedProperty">The resolved property.</param>
    /// <returns>
    ///     A new instance of <see cref="ToConstructorResolutionOptions"/>.
    /// </returns>
    public static ToConstructorResolutionOptions Resolves(PropertyOptions resolvedProperty)
    {
        return new(resolvedProperty);
    }
    
    private ToConstructorResolutionOptions(PropertyOptions resolvedProperty) 
        : base(resolvedProperty)
    {
        Status = ResolutionStatus.MappedToConstructor;
    }
}
