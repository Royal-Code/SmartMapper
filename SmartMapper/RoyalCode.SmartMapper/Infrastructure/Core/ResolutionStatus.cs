namespace RoyalCode.SmartMapper.Infrastructure.Core;

/// <summary>
/// Resolution Status of a property.
/// </summary>
public enum ResolutionStatus
{
    /// <summary>
    /// The property mapping is not resolved.
    /// </summary>
    Undefined,
    
    /// <summary>
    /// The property will be ignored.
    /// </summary>
    Ignored,
    
    /// <summary>
    /// The property is mapped to a destination method parameter.
    /// </summary>
    MappedToMethodParameter,

    /// <summary>
    /// The property is mapped to a constructor parameter of the destination type.
    /// </summary>
    MappedToConstructor,

    /// <summary>
    /// The property is mapped to a property of the destination type.
    /// </summary>
    MappedToProperty,
    
}