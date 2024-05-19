using RoyalCode.SmartMapper.Adapters.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Options.Resolutions;

/// <summary>
/// Options that contains the resolution of a property to a constructor parameter.
/// </summary>
public class ToConstructorParameterResolutionOptions : ParameterResolutionOptionsBase
{
    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="ToConstructorParameterResolutionOptions"/>.
    /// </para>
    /// <para>
    ///     Call <see cref="PropertyOptions.ResolvedBy(ResolutionOptionsBase)"/> to add the current instance
    ///     as a resolution of the source property.
    /// </para>
    /// </summary>
    /// <param name="resolvedProperty">The resolved property.</param>
    /// <param name="toConstructorParameterOptions">The options for the constructor parameter.</param>
    /// <returns>
    ///     A new instance of <see cref="ToConstructorParameterResolutionOptions"/>.
    /// </returns>
    public static  ToConstructorParameterResolutionOptions Resolves(
        PropertyOptions resolvedProperty,
        ToConstructorParameterOptions toConstructorParameterOptions)
    {
        return new(resolvedProperty, toConstructorParameterOptions);
    } 
    
    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="ToConstructorParameterResolutionOptions"/>.
    /// </para>
    /// <para>
    ///     Call <see cref="PropertyOptions.ResolvedBy(ResolutionOptionsBase)"/> to add the current instance
    ///     as a resolution of the source property.
    /// </para>
    /// </summary>
    /// <param name="resolvedProperty">The resolved property.</param>
    /// <param name="toConstructorParameterOptions">The options for the constructor parameter.</param>
    private ToConstructorParameterResolutionOptions(
        PropertyOptions resolvedProperty, 
        ToConstructorParameterOptions toConstructorParameterOptions) 
        : base(resolvedProperty, toConstructorParameterOptions)
    {
        ToConstructorParameterOptions = toConstructorParameterOptions;
        Status = ResolutionStatus.MappedToConstructorParameter;
    }

    /// <summary>
    /// The options for the constructor parameter.
    /// </summary>
    public ToConstructorParameterOptions ToConstructorParameterOptions { get; }
    
    /// <summary>
    /// <para>
    ///     The possible index of the constructor parameter.
    /// </para>
    /// <para>
    ///     If the parameter can not be resolved by the name, the index will be used.
    /// </para>
    /// <para>
    ///     When the value is less than 0, the index will not be used.
    /// </para>
    /// </summary>
    public int Sequence { get; set; } = -1;
}
