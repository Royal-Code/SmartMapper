using RoyalCode.SmartMapper.Adapters.Resolvers.Avaliables;
using RoyalCode.SmartMapper.Adapters.Resolvers.Targets;
using RoyalCode.SmartMapper.Core.Configurations;

namespace RoyalCode.SmartMapper.Adapters.Discovery.Parameters;

/// <summary>
/// A request to discover the property to parameter assignments for the mapper.
/// </summary>
/// <param name="Configurations">The mapper configurations.</param>
/// <param name="TargetParameter">The target parameter to discover the assignments.</param>
/// <param name="AvailableSourceItems">The available source items to discover the assignments.</param>
public sealed record ToParameterDiscoveryRequest(
    MapperConfigurations Configurations,
    TargetParameter TargetParameter,
    AvailableSourceItems AvailableSourceItems);
