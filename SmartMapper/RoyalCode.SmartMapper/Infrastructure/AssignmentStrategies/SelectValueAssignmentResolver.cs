using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;

public class SelectValueAssignmentResolver : IValueAssignmentResolver
{
    public ValueAssignmentStrategy Strategy => ValueAssignmentStrategy.Select;
    
    public AssignmentResolution Resolve(AssignmentContext context)
    {
        throw new NotImplementedException();
    }

    public bool TryResolve(AssignmentContext context, out AssignmentResolution? resolution)
    {
        throw new NotImplementedException();
    }
}