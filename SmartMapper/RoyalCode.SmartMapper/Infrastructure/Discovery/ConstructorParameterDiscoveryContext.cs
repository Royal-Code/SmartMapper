using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;
using RoyalCode.SmartMapper.Infrastructure.Configurations;

namespace RoyalCode.SmartMapper.Infrastructure.Discovery;

public record ConstructorParameterDiscoveryContext(
    IEnumerable<SourceProperty> SourceProperties, // TODO: use of adapter namespace
    IEnumerable<TargetParameter> Parameters, // TODO: use of adapter namespace
    ResolutionConfiguration Configuration);