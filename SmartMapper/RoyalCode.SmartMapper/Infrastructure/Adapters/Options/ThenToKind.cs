namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

/// <summary>
/// Used by <see cref="ThenOptions"/> to specify if the target property has
/// a continuation to another property or method.
/// </summary>
public enum ThenToKind
{
    /// <summary>
    /// Continues to another property.
    /// </summary>
    Property,
    
    /// <summary>
    /// Continues to a method.
    /// </summary>
    Method
}