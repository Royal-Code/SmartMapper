using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Parameters;

/// <summary>
/// A request to discover the property to parameter assignments for the mapper.
/// </summary>
/// <param name="configurations">The mapper configurations.</param>
/// <param name="availableParameter">The target parameter to discover the assignments.</param>
/// <param name="availableSourceItems">The available source items to discover the assignments.</param>
public readonly struct ToParameterDiscoveryRequest(
    MapperConfigurations configurations,
    AvailableParameter availableParameter,
    AvailableSourceItems availableSourceItems)
{
    /// <summary>
    /// The mapper configurations.
    /// </summary>
    public MapperConfigurations Configurations { get; } = configurations;

    /// <summary>
    /// The target parameter to discover the assignments.
    /// </summary>
    public AvailableParameter AvailableParameter { get; } = availableParameter;

    /// <summary>
    /// The available source items to discover the assignments.
    /// </summary>
    public AvailableSourceItems AvailableSourceItems { get; } = availableSourceItems;
}
