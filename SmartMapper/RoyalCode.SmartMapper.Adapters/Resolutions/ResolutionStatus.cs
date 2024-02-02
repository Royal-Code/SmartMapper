
namespace RoyalCode.SmartMapper.Adapters.Resolutions;

/// <summary>
/// <para>
///     Resolution Status of a property.
/// </para>
/// <para>
///     Determines where the property of the source object will be mapped to.
/// </para>
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
    /// The property is mapped to a constructor parameter of the destination type.
    /// </summary>
    MappedToConstructorParameter,

    /// <summary>
    /// The property is mapped to a destination method parameter.
    /// </summary>
    MappedToMethodParameter,

    /// <summary>
    /// The property is mapped to a property of the destination type.
    /// </summary>
    MappedToProperty,

    /// <summary>
    /// The property is mapped to a constructor of the destination type,
    /// where the inner/internal properties is mapped to the constructor parameter.
    /// </summary>
    MappedToConstructor,
    
    /// <summary>
    /// The property is mapped to a method of the destination type,
    /// where the inner/internal properties is mapped to the method parameter.
    /// </summary>
    MappedToMethod,

    /// <summary>
    /// The is mapped to the destination type
    /// where the inner/internal properties is mapped to properties of the destination type.
    /// </summary>
    MappedToTarget,
}