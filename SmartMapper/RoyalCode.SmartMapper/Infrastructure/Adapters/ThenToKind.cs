namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

/// <summary>
/// Used by <see cref="ToPropertyTargetRelatedOptionsBase"/> to specify if the target property has
/// a continuation to another property or method.
/// </summary>
public enum ThenToKind
{
    /// <summary>
    /// None continuation is applied.
    /// </summary>
    NotApplied,
    
    /// <summary>
    /// Continues to another property.
    /// </summary>
    Property,
    
    /// <summary>
    /// Continues to a method.
    /// </summary>
    Method
}