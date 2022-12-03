using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;
using RoyalCode.SmartMapper.Infrastructure.Core;
using RoyalCode.SmartMapper.Infrastructure.Resolvers;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;

public class ParameterResolution : ResolutionBase
{
    public ParameterResolution(AvailableSourceProperty availableSourceProperty)
    {
        AvailableSourceProperty = availableSourceProperty;
    }
    
    public AvailableSourceProperty AvailableSourceProperty { get; }
    public AssignmentResolution? AssignmentResolution { get; init; }
    public TargetParameter? Parameter { get; init; }

    
    public ToConstructorParameterOptions? ToParameterOptions { get; init; }
}