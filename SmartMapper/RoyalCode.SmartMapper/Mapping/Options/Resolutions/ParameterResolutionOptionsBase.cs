namespace RoyalCode.SmartMapper.Mapping.Options.Resolutions;

/// <summary>
/// /// <para>
///     This base options is for all the options that are related to assigning values from the source property
///     to some destination method parameter or constructor parameter.
/// </para>
/// </summary>
public abstract class ParameterResolutionOptionsBase : ResolutionOptionsBase
{
    /// <summary>
    /// <para>
    ///     Base constructor for the parameter resolution options.
    /// </para>
    /// <para>
    ///     Call <see cref="PropertyOptions.ResolvedBy(ResolutionOptionsBase)"/> to add the current instance
    ///     as a resolution of the source property.
    /// </para>
    /// </summary>
    /// <param name="resolvedProperty">The source property related to the assignment.</param>
    /// <param name="toParameterOptions">The options to assign the source property to the parameter.</param>
    protected ParameterResolutionOptionsBase(PropertyOptions resolvedProperty, ParameterOptionsBase toParameterOptions)
        : base(resolvedProperty)
    {
        ToParameterOptions = toParameterOptions;
    }

    /// <summary>
    /// The options to assign the source property to the parameter.
    /// </summary>
    public ParameterOptionsBase ToParameterOptions { get; }
}
