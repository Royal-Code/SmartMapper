
using RoyalCode.SmartMapper.Mapping.Options;

namespace RoyalCode.SmartMapper.Adapters.Options.Resolutions;

/// <summary>
/// Resolution options for a property that is ignored.
/// </summary>
public sealed class IgnoreResolutionOptions : ResolutionOptionsBase
{
    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="IgnoreResolutionOptions"/> that resolves the source property.
    /// </para>
    /// <para>
    ///     Call <see cref="PropertyOptions.ResolvedBy(ResolutionOptionsBase)"/> to add the current instance
    ///     as a resolution of the source property.
    /// </para>
    /// </summary>
    /// <param name="resolvedProperty">The resolved property options.</param>
    /// <returns>
    ///     A new instance of <see cref="IgnoreResolutionOptions"/> that resolves the source property.
    /// </returns>
    public static IgnoreResolutionOptions Resolves(PropertyOptions resolvedProperty)
    {
        return new IgnoreResolutionOptions(resolvedProperty);
    }
    
    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="IgnoreResolutionOptions"/>.
    /// </para>
    /// <para>
    ///     Call <see cref="PropertyOptions.ResolvedBy(ResolutionOptionsBase)"/> to add the current instance
    ///     as a resolution of the source property.
    /// </para>
    /// </summary>
    /// <param name="resolvedProperty">The source property related to the assignment.</param>
    private IgnoreResolutionOptions(PropertyOptions resolvedProperty) 
        : base(resolvedProperty)
    {
        Status = Adapters.Resolutions.ResolutionStatus.Ignored;
    }
}
