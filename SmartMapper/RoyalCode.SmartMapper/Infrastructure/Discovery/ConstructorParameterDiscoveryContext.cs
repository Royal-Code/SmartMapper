using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;
using RoyalCode.SmartMapper.Infrastructure.Configurations;

namespace RoyalCode.SmartMapper.Infrastructure.Discovery;

public record ConstructorParameterDiscoveryContext(
    IEnumerable<AvailableSourceProperty> AvailableProperties, // TODO: use of adapter namespace
    IEnumerable<TargetParameter> Parameters, // TODO: use of adapter namespace
    ResolutionConfiguration Configuration);