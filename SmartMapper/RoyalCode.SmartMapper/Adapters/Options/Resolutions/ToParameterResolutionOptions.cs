using RoyalCode.SmartMapper.Adapters.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Options.Resolutions;

/// <summary>
/// Options that container information about how to resolve a parameter.
/// </summary>
public class ToParameterResolutionOptions : ResolutionOptionsBase
{
    /// <summary>
    /// Creates a new instance of <see cref="ToParameterResolutionOptions"/>.
    /// </summary>
    /// <param name="resolvedProperty">The resolved property.</param>
    /// <param name="toConstructorParameterOptions">The options for the constructor parameter.</param>
    public ToParameterResolutionOptions(
        PropertyOptions resolvedProperty, 
        ToConstructorParameterOptions toConstructorParameterOptions) : base(resolvedProperty)
    {
        ToConstructorParameterOptions = toConstructorParameterOptions;
        Status = ResolutionStatus.MappedToConstructorParameter;
    }

    /// <summary>
    /// The options for the constructor parameter.
    /// </summary>
    public ToConstructorParameterOptions ToConstructorParameterOptions { get; }
}
