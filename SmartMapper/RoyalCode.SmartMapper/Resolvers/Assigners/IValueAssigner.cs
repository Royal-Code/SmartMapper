using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Resolvers.Assigners;

public interface IValueAssigner
{
    ValueAssignmentStrategy Strategy { get; }

    void GetValueExpression(); // ??? how will the expression be created?
}
