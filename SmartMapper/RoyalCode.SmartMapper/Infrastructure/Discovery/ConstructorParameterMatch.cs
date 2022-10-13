using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;
using RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;

namespace RoyalCode.SmartMapper.Infrastructure.Discovery;

public record ConstructorParameterMatch(
    AvailableSourceProperty Property,
    TargetParameter Parameter,
    AssignmentResolution AssignmentResolution);