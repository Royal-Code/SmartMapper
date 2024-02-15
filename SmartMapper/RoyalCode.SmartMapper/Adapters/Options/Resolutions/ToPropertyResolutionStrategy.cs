
namespace RoyalCode.SmartMapper.Adapters.Options.Resolutions;

/// <summary>
/// The resolution strategy when a source property is mapped to a target property.
/// </summary>
public enum ToPropertyResolutionStrategy
{
    /// <summary>
    /// The source property value will be setted to the target property.
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
