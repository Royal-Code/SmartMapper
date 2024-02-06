
namespace RoyalCode.SmartMapper.Core.Resolutions;

/// <summary>
/// <para>
///     Represents the resolution of the strategy used to assign the value of the source property to the destination property or parameter.
/// </para>
/// </summary>
/// <param name="Resolution">
///     The resolution of the assignment between source type and destination type.
/// </param>
/// <param name="Converter">
///     A converter, used to convert the value of the source type to the target type.
/// </param>
public sealed record AssignmentStrategyResolution(
    ValueAssignmentResolution Resolution, 
    ValueAssignmentConverter? Converter = null);