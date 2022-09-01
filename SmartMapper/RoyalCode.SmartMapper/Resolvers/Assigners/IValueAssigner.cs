using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RoyalCode.SmartMapper.Resolvers.Assigners;

public interface IValueAssigner
{
    ValueAssignmentStrategy Strategy { get; }

    void GetValueExpression(); // ??? how will the expression be created?
}
