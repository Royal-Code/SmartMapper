
using RoyalCode.SmartMapper.Resolvers.Assigners;
using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartMapper.Resolvers.AssignmentStrategies;

public class ConvertStrategy : IAssignmentStrategy
{
    public bool CanResolve(
        Type sourceType,
        Type targetType,
        [NotNullWhen(true)] out IValueAssigner? valueAssigner)
    {



        throw new NotImplementedException();
    }
}
