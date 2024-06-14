namespace RoyalCode.SmartMapper.Mapping.Options.Resolutions;

/// <summary>
/// The resolution strategy when a source property is mapped to a target property.
/// </summary>
public enum ToPropertyResolutionStrategy
{
    /// <summary>
    /// The source property value will be set to the target property. This is the default strategy.
    /// </summary>
    SetValue,

    /// <summary>
    /// An internal property of the target property will be accessed.
    /// </summary>
    AccessInnerProperty,

    /// <summary>
    /// A method of the target property will be called.
    /// </summary>
    CallMethod,
}
