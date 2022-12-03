using RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;
using RoyalCode.SmartMapper.Infrastructure.Resolvers;

namespace RoyalCode.SmartMapper.Infrastructure.Discovery;

public record ConstructorParameterMatch(
    AvailableSourceProperty Property,
    TargetParameter Parameter,
    AssignmentResolution AssignmentResolution);