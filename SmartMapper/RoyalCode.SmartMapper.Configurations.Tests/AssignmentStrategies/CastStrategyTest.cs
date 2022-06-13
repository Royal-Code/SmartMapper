
using RoyalCode.SmartMapper.Resolvers.AssignmentStrategies;
using Xunit;

namespace RoyalCode.SmartMapper.Configurations.Tests.AssignmentStrategies;

public class CastStrategyTest
{
    private readonly CastStrategy assignmentStrategy = new();

    [Fact]
    public void When_SameType_NotAccept()
    {
        var accept = assignmentStrategy.CanResolve(typeof(A), typeof(A), out _);
        Assert.False(accept);
    }

    private enum A
    {
        Red = 0,
        Green = 1
    }

    private enum B
    {
        Red = 0,
        Green = 1
    }
}
