namespace RoyalCode.SmartMapper.Infrastructure.Core;

/// <summary>
/// Defines the strategy for dealing with the assignment between source type and destination type in a mapping.
/// </summary>
public enum ValueAssignmentStrategy
{
    /// <summary>
    /// Not yet defined.
    /// </summary>
    Undefined,
    
    /// <summary>
    /// Direct set value.
    /// </summary>
    Direct,
    
    /// <summary>
    /// Cast the source type to the target type.
    /// </summary>
    Cast,
    
    /// <summary>
    /// Convert the value of source type to the target type.
    /// </summary>
    Convert,
    
    /// <summary>
    /// Uses a service to process the source type value to generate a target type value.
    /// </summary>
    Processor,
    
    /// <summary>
    /// Adapt the source type to the target type.
    /// </summary>
    Adapt,
    
    // TODO: MapOrAdapt,
    
    /// <summary>
    /// Select the target type from the source type.
    /// </summary>
    Select,
}