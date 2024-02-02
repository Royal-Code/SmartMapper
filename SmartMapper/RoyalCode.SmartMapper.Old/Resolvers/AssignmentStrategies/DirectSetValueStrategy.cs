using RoyalCode.SmartMapper.Resolvers.Assigners;
using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartMapper.Resolvers.AssignmentStrategies;

public class DirectSetValueStrategy : IAssignmentStrategy
{
    public bool CanResolve(Type sourceType, Type targetType, [NotNullWhen(true)] out IValueAssigner? valueAssigner)
    {
        if (targetType.IsAssignableFrom(sourceType))
        {
            valueAssigner = new DirectSetValueAssigner();
            return true;
        }

        valueAssigner = null;
        return false;
    }
}