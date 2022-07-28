namespace RoyalCode.SmartMapper.Infrastructure.Core;

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
    
}