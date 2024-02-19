using RoyalCode.SmartMapper.Adapters.Options.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Options;

/// <summary>
/// Options to resolve a source property to call a method of a target property.
/// </summary>
public sealed class ThenToMethodOptions
{
    /// <summary>
    /// Creates a new instance of <see cref="ThenToMethodOptions"/>.
    /// </summary>
    /// <param name="propertyResolutionOptions"></param>
    /// <param name="toMethodParameterOptions"></param>
    public ThenToMethodOptions(
        ToPropertyResolutionOptions propertyResolutionOptions,
        ToMethodParameterOptions toMethodParameterOptions)
    {
        PropertyResolutionOptions = propertyResolutionOptions;
        ToMethodParameterOptions = toMethodParameterOptions;
    }
    
    /// <summary>
    /// The property resolution options.
    /// </summary>
    public ToPropertyResolutionOptions PropertyResolutionOptions { get; }

    /// <summary>
    /// The source property options.
    /// </summary>
    public PropertyOptions SourcePropertyOptions => PropertyResolutionOptions.ResolvedProperty;
    
    /// <summary>
    /// The options for the parameter method.
    /// </summary>
    public ToMethodParameterOptions ToMethodParameterOptions { get; }
}