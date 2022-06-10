using RoyalCode.SmartMapper.Resolvers.Assigners;
using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartMapper.Resolvers.AssignmentStrategies;

public class CastStrategy : IAssignmentStrategy
{
    public bool CanResolve(Type sourceType, Type targetType, [NotNullWhen(true)] out IValueAssigner? valueAssigner)
    {
        valueAssigner = null;
        if (targetType.IsAssignableFrom(sourceType))
            return false;

        if (sourceType.IsEnum && targetType.IsEnum
            && Enum.GetUnderlyingType(sourceType) == Enum.GetUnderlyingType(targetType))
        {
            valueAssigner = new CastValueAssigner();
            return true;
        }

        return false;
    }
}
