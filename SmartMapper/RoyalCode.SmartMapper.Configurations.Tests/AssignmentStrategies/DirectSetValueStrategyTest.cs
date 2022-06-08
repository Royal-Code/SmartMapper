using RoyalCode.SmartMapper.Resolvers;
using Xunit;

namespace RoyalCode.SmartMapper.Configurations.Tests.AssignmentStrategies;

public class DirectSetValueStrategyTest
{
    private readonly DirectSetValueStrategy assignmentStrategy = new();

    [Fact]
    public void When_SameTypes_Assign()
    {
        var assign = assignmentStrategy.TryResolve(
            new AssignmentOptions(),
            typeof(DirectSetValueStrategy),
            typeof(DirectSetValueStrategy));

        Assert.True(assign);
    }

    [Fact]
    public void When_SuperType_Assign()
    {
        var assign = assignmentStrategy.TryResolve(
            new AssignmentOptions(),
            typeof(DirectSetValueStrategy),
            typeof(IAssignmentStrategy));

        Assert.True(assign);
    }

    [Fact]
    public void When_ConcreteTypeAgainstSuperType_Error()
    {
        var assign = assignmentStrategy.TryResolve(
            new AssignmentOptions(),
            typeof(IAssignmentStrategy),
            typeof(DirectSetValueStrategy));

        Assert.False(assign);
    }

    [Fact]
    public void When_SuperType_With_SameGeneric_Assign()
    {
        var assign = assignmentStrategy.TryResolve(
            new AssignmentOptions(),
            typeof(ConcretDirectStrategyGenericTest<IAssignmentStrategy>),
            typeof(IDirectStrategyGenericTest<IAssignmentStrategy>));

        Assert.True(assign);
    }

    [Fact]
    public void When_SuperType_With_ContravarianceGeneric_Assign()
    {
        var assign = assignmentStrategy.TryResolve(
            new AssignmentOptions(),
            typeof(ConcretDirectStrategyGenericTest<IAssignmentStrategy>),
            typeof(IDirectStrategyGenericTest<DirectSetValueStrategy>));

        Assert.True(assign);
    }

    [Fact]
    public void When_SuperType_With_ReverseContravarianceGeneric_Error()
    {
        var assign = assignmentStrategy.TryResolve(
            new AssignmentOptions(),
            typeof(ConcretDirectStrategyGenericTest<DirectSetValueStrategy>),
            typeof(IDirectStrategyGenericTest<IAssignmentStrategy>));

        Assert.False(assign);
    }

    [Fact]
    public void When_SuperType_With_CovarianceGeneric_Assign()
    {
        var assign = assignmentStrategy.TryResolve(
            new AssignmentOptions(),
            typeof(ConcretDirectStrategyGenericTestCo<DirectSetValueStrategy>),
            typeof(IDirectStrategyGenericTestCo<IAssignmentStrategy>));

        Assert.True(assign);
    }

    [Fact]
    public void When_SuperType_With_ReverseCovarianceGeneric_Error()
    {
        var assign = assignmentStrategy.TryResolve(
            new AssignmentOptions(),
            typeof(ConcretDirectStrategyGenericTestCo<IAssignmentStrategy>),
            typeof(IDirectStrategyGenericTestCo<DirectSetValueStrategy>));

        Assert.False(assign);
    }

    private interface IDirectStrategyGenericTest<in T>
    {
        void Action(T obj);
    }

    private interface IDirectStrategyGenericTestCo<out T>
    {
        T Get();
    }

    private class ConcretDirectStrategyGenericTest<T> : IDirectStrategyGenericTest<T>
    {
        public void Action(T obj) { }
    }

    private class ConcretDirectStrategyGenericTestCo<T> : IDirectStrategyGenericTestCo<T>
    {
        public T Get()
        {
            return default;
        }
    }
}
