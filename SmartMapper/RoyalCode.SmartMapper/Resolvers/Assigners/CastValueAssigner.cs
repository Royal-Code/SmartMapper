
using RoyalCode.SmartMapper.Infrastructure.Adapters;

namespace RoyalCode.SmartMapper.Resolvers.Assigners;

public class CastValueAssigner : IValueAssigner
{
    public ValueAssignmentStrategy Strategy => ValueAssignmentStrategy.Cast;

    public void GetValueExpression()
    {
        throw new NotImplementedException();
    }
}
