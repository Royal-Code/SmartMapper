using RoyalCode.SmartMapper.Infrastructure.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Resolvers;

namespace RoyalCode.SmartMapper.Infrastructure.Discovery;


public record ParameterDiscoveryRequest(
    IEnumerable<AvailableSourceProperty> AvailableProperties,
    IEnumerable<TargetParameter> Parameters,
    ResolutionConfiguration Configuration);