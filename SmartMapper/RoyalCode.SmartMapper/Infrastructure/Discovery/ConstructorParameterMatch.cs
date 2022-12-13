using RoyalCode.SmartMapper.Infrastructure.Resolvers;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.AssignmentStrategies;

namespace RoyalCode.SmartMapper.Infrastructure.Discovery;

public record ConstructorParameterMatch(
    AvailableSourceProperty Property,
    TargetParameter Parameter,
    AssignmentResolution AssignmentResolution);