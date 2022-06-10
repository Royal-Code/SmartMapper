using RoyalCode.SmartMapper.Configurations;

namespace RoyalCode.SmartMapper.Resolvers;

public class AssignmentResolver
{
    private readonly IResolversManager resolversManager;

    public AssignmentResolver(IResolversManager resolversManager)
    {
        this.resolversManager = resolversManager;
    }
    
    public void Resolve(AssignmentOptions options, Type sourceType, Type targetType)
    {
        foreach (var assignmentStrategy in resolversManager.AssignmentStrategies)
        {
            if (assignmentStrategy.CanResolve(sourceType, targetType, out var assigner))
            {
                options.Assigner = assigner;
                return;
            }
        }
    }
}
