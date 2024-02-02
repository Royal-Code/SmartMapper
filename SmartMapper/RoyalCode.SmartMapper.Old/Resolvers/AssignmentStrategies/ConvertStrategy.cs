
using RoyalCode.SmartMapper.Resolvers.Assigners;
using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Resolvers.Converters;

namespace RoyalCode.SmartMapper.Resolvers.AssignmentStrategies;

public class ConvertStrategy : IAssignmentStrategy
{
    public static readonly List<IValueConverter> Converters = new();
    
    public bool CanResolve(
        Type sourceType,
        Type targetType,
        [NotNullWhen(true)] out IValueAssigner? valueAssigner)
    {
        valueAssigner = null;
        if (targetType.IsAssignableFrom(sourceType))
            return false;

        

        throw new NotImplementedException();
    }
}
