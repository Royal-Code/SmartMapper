using RoyalCode.SmartMapper.Adapters.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Options.Resolutions;

/// <summary>
/// Options that contains the resolution of a property to a method parameter.
/// </summary>
public sealed class ToMethodParameterResolutionOptions : ParameterResolutionOptionsBase
{
    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="ToMethodParameterResolutionOptions"/>.
    /// </para>
    /// </summary>
    /// <param name="resolvedProperty">The resolved source property options.</param>
    /// <param name="toParameterOptions">The parameter resolution options.</param>
    public ToMethodParameterResolutionOptions(
        PropertyOptions resolvedProperty,
        ToMethodParameterOptions toParameterOptions) 
        : base(resolvedProperty, toParameterOptions)
    {
        ToMethodParameterOptions = toParameterOptions;
        Status = ResolutionStatus.MappedToMethodParameter;
    }

    /// <summary>
    /// The options for the method parameter.
    /// </summary>
    public ToMethodParameterOptions ToMethodParameterOptions { get; }
}
