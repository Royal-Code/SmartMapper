
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Resolvers.Assigners;

public class CastValueAssigner : IValueAssigner
{
    public ValueAssignmentStrategy Strategy => ValueAssignmentStrategy.Cast;

    public void GetValueExpression()
    {
        throw new NotImplementedException();
    }
}
