using RoyalCode.SmartMapper.Core.Configurations;

namespace RoyalCode.SmartMapper.Core.Discovery.Assignment;

/// <summary>
/// Represents the request for the assignment discovery process.
/// </summary>
/// <param name="Configurations">The configurations for the mapper.</param>
/// <param name="SourceType">The value type of the source property.</param>
/// <param name="TargetType">The value type of the target property or parameter.</param>
public record AssignmentDiscoveryRequest(MapperConfigurations Configurations, Type SourceType, Type TargetType);
