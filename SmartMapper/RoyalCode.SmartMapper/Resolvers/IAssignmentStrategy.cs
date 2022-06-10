using RoyalCode.SmartMapper.Resolvers.Assigners;
using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartMapper.Resolvers;

public interface IAssignmentStrategy
{
    bool CanResolve(Type sourceType, Type targetType, [NotNullWhen(true)] out IValueAssigner? valueAssigner);
}
