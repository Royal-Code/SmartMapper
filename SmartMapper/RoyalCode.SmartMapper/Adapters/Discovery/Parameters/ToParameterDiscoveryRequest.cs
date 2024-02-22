using RoyalCode.SmartMapper.Adapters.Resolvers.Avaliables;
using RoyalCode.SmartMapper.Adapters.Resolvers.Targets;
using RoyalCode.SmartMapper.Core.Configurations;

namespace RoyalCode.SmartMapper.Adapters.Discovery.Parameters;

/// <summary>
/// A request to discover the property to parameter assignments for the mapper.
/// </summary>
/// <param name="configurations">The mapper configurations.</param>
/// <param name="targetParameter">The target parameter to discover the assignments.</param>
/// <param name="availableSourceItems">The available source items to discover the assignments.</param>
public readonly struct ToParameterDiscoveryRequest(
    MapperConfigurations configurations,
    TargetParameter targetParameter,
    AvailableSourceItems availableSourceItems)
{
    /// <summary>
    /// The mapper configurations.
    /// </summary>
    public MapperConfigurations Configurations { get; } = configurations;

    /// <summary>
    /// The target parameter to discover the assignments.
    /// </summary>
    public TargetParameter TargetParameter { get; } = targetParameter;

    /// <summary>
    /// The available source items to discover the assignments.
    /// </summary>
    public AvailableSourceItems AvailableSourceItems { get; } = availableSourceItems;
}
