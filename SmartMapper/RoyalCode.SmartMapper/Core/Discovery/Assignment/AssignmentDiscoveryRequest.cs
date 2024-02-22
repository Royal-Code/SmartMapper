using RoyalCode.SmartMapper.Core.Configurations;

namespace RoyalCode.SmartMapper.Core.Discovery.Assignment;

/// <summary>
/// Represents the request for the assignment discovery process.
/// </summary>
/// <param name="configurations">The configurations for the mapper.</param>
/// <param name="sourceType">The value type of the source property.</param>
/// <param name="targetType">The value type of the target property or parameter.</param>
public readonly struct AssignmentDiscoveryRequest(
    MapperConfigurations configurations,
    Type sourceType,
    Type targetType)
{
    /// <summary>
    /// The configurations for the mapper.
    /// </summary>
    public MapperConfigurations Configurations { get; } = configurations;

    /// <summary>
    /// The value type of the source property.
    /// </summary>
    public Type SourceType { get; } = sourceType;

    /// <summary>
    /// The value type of the target property or parameter.
    /// </summary>
    public Type TargetType { get; } = targetType;
}
