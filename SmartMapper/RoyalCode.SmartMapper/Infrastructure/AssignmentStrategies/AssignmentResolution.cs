using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;

public class AssignmentResolution : ResolutionBase
{
    public ValueAssignmentStrategy Strategy { get; init; }
}