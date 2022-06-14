
using RoyalCode.SmartMapper.Resolvers.Assigners;
using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartMapper.Resolvers.AssignmentStrategies;

public class AdaptStrategy : IAssignmentStrategy
{
    private readonly IResolversManager resolversManager;

    public AdaptStrategy(IResolversManager resolversManager)
    {
        this.resolversManager = resolversManager;
    }

    public bool CanResolve(Type sourceType, Type targetType, [NotNullWhen(true)] out IValueAssigner? valueAssigner)
    {
        var result = resolversManager.ResolveAdapter(sourceType, targetType);

        if (result.Success)
        {
            valueAssigner = new AdapterValueAssigner(result);
            return true;
        }

        valueAssigner = null;
        return false;
    }
}
