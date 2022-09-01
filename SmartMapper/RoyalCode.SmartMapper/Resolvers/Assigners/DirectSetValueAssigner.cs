using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RoyalCode.SmartMapper.Resolvers.Assigners;

public class DirectSetValueAssigner : IValueAssigner
{
    public ValueAssignmentStrategy Strategy => ValueAssignmentStrategy.Direct;

    public void GetValueExpression()
    {
        throw new NotImplementedException();
    }
}