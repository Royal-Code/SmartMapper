namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

/// <summary>
/// Strategy used to map properties from a source object as parameters to a destination object method.
/// </summary>
public enum ParametersStrategy
{
    /// <summary>
    /// Map only the selected properties as parameters.
    /// </summary>
    SelectedParameters,
    
    /// <summary>
    /// Mapp all properties as parameters.
    /// </summary>
    AllParameters
}