namespace RoyalCode.SmartMapper.Mapping.Options.Resolutions;

/// <summary>
/// Options for the resolution of inner properties, where the inner properties are mapped to the target.
/// </summary>
public class InnerPropertiesResolutionOptions : InnerPropertiesResolutionOptionsBase
{
    /// <summary>
    /// <para>
    ///     Creates a new <see cref="InnerPropertiesResolutionOptions"/> that resolves the source property.
    /// </para>
    /// <para>
    ///     Call <see cref="PropertyOptions.ResolvedBy(ResolutionOptionsBase)"/> to add the current instance
    ///     as a resolution of the source property.
    /// </para>
    /// </summary>
    /// <param name="resolvedProperty">The resolved property options.</param>
    /// <returns>
    ///     A new instance of <see cref="InnerPropertiesResolutionOptions"/> that resolves the source property.
    /// </returns>
    public static InnerPropertiesResolutionOptions Resolves(PropertyOptions resolvedProperty)
    {
        return new(resolvedProperty);
    }

    /// <summary>
    /// Creates a new instance of <see cref="InnerPropertiesResolutionOptions"/>.
    /// </summary>
    /// <param name="resolvedProperty">The resolved property options.</param>
    private InnerPropertiesResolutionOptions(PropertyOptions resolvedProperty)
        : base(resolvedProperty)
    {
        Status = ResolutionStatus.MappedToTarget;
    }
}