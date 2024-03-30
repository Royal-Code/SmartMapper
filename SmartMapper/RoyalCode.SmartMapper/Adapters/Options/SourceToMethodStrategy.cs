namespace RoyalCode.SmartMapper.Adapters.Options;

/// <summary>
/// Strategy used to map properties from a source object as parameters to a destination object method.
/// </summary>
public enum SourceToMethodStrategy
{
    /// <summary>
    /// Default value, same behaviour as <see cref="AllParameters"/>.
    /// </summary>
    Default,
    
    /// <summary>
    /// Map only the selected properties as parameters.
    /// </summary>
    SelectedParameters,
    
    /// <summary>
    /// Map all available properties as parameters.
    /// </summary>
    AllParameters
}