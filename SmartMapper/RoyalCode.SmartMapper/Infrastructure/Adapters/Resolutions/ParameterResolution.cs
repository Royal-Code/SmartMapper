using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;
using RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;

public class ParameterResolution : ResolutionBase
{
    public AvailableSourceProperty AvailableSourceProperty { get; init; }
    public AssignmentResolution? AssignmentResolution { get; init; }
    public TargetParameter? Parameter { get; init; }
    public ToConstructorParameterOptions? ToParameterOptions { get; init; }
}