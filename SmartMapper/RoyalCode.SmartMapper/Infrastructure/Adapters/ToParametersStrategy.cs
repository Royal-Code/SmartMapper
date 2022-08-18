namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

/// <summary>
/// The strategy to use when mapping a property to a method or constructor.
/// </summary>
public enum ToParametersStrategy
{
    /// <summary>
    /// Undefined strategy.
    /// </summary>
    Undefined,
    
    /// <summary>
    /// Map the property to the first parameter of the method.
    /// </summary>
    Value,
    
    /// <summary>
    /// Map inner/internal properties to the parameters of the method or constructor.
    /// </summary>
    InnerProperties,
}