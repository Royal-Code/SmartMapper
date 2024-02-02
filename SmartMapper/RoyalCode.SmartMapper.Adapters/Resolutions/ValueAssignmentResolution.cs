
namespace RoyalCode.SmartMapper.Adapters.Resolutions;

/// <summary>
/// Defines the resolution for dealing with the assignment between source type and destination type in a mapping.
/// </summary>
public enum ValueAssignmentResolution
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
    /// Adapt the source type to the target type.
    /// </summary>
    Adapt,

    /// <summary>
    /// Select the target type from the source type.
    /// </summary>
    Select,
}