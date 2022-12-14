using RoyalCode.SmartMapper.Infrastructure.Core;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.AssignmentStrategies;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.Parameters;

public class ParameterResolution : ResolutionBase
{
    public ParameterResolution(AvailableSourceProperty availableSourceProperty)
    {
        AvailableSourceProperty = availableSourceProperty;
    }

    public AvailableSourceProperty AvailableSourceProperty { get; }
    public AssignmentResolution? AssignmentResolution { get; init; }
    public TargetParameter? Parameter { get; init; }


    //public ToConstructorParameterOptions? ToParameterOptions { get; init; }
}