
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Resolvers.Assigners;

public class AdapterValueAssigner : IValueAssigner
{
    private readonly AdaptResolveResult adaptResolveResult;

    public AdapterValueAssigner(AdaptResolveResult adaptResolveResult)
    {
        this.adaptResolveResult = adaptResolveResult;
    }

    public ValueAssignmentStrategy Strategy => ValueAssignmentStrategy.Adapt;

    public void GetValueExpression()
    {
        throw new NotImplementedException();
    }
}
