using RoyalCode.SmartMapper.Resolvers;
using RoyalCode.SmartMapper.Resolvers.AssignmentStrategies;
using System;
using Xunit;

namespace RoyalCode.SmartMapper.Configurations.Tests.AssignmentStrategies;

public class DirectSetValueStrategyTest
{
    private readonly DirectSetValueStrategy assignmentStrategy = new();

    [Fact]
    public void When_SameTypes_Assign()
    {
        var assign = assignmentStrategy.CanResolve(
            typeof(DirectSetValueStrategy),
            typeof(DirectSetValueStrategy), out _);

        Assert.True(assign);
    }

    [Fact]
    public void When_SuperType_Assign()
    {
        var assign = assignmentStrategy.CanResolve(
            typeof(DirectSetValueStrategy),
            typeof(IAssignmentStrategy), out _);

        Assert.True(assign);
    }

    [Fact]
    public void When_ConcreteTypeAgainstSuperType_Error()
    {
        var assign = assignmentStrategy.CanResolve(
            typeof(IAssignmentStrategy),
            typeof(DirectSetValueStrategy), out _);

        Assert.False(assign);
    }

    [Fact]
    public void When_SuperType_With_SameGeneric_Assign()
    {
        var assign = assignmentStrategy.CanResolve(
            typeof(ConcretDirectStrategyGenericTest<IAssignmentStrategy>),
            typeof(IDirectStrategyGenericTest<IAssignmentStrategy>), out _);

        Assert.True(assign);
    }

    [Fact]
    public void When_SuperType_With_ContravarianceGeneric_Assign()
    {
        var assign = assignmentStrategy.CanResolve(
            typeof(ConcretDirectStrategyGenericTest<IAssignmentStrategy>),
            typeof(IDirectStrategyGenericTest<DirectSetValueStrategy>), out _);

        Assert.True(assign);
    }

    [Fact]
    public void When_SuperType_With_ReverseContravarianceGeneric_Error()
    {
        var assign = assignmentStrategy.CanResolve(
            typeof(ConcretDirectStrategyGenericTest<DirectSetValueStrategy>),
            typeof(IDirectStrategyGenericTest<IAssignmentStrategy>), out _);

        Assert.False(assign);
    }

    [Fact]
    public void When_SuperType_With_CovarianceGeneric_Assign()
    {
        var assign = assignmentStrategy.CanResolve(
            typeof(ConcretDirectStrategyGenericTestCo<DirectSetValueStrategy>),
            typeof(IDirectStrategyGenericTestCo<IAssignmentStrategy>), out _);

        Assert.True(assign);
    }

    [Fact]
    public void When_SuperType_With_ReverseCovarianceGeneric_Error()
    {
        var assign = assignmentStrategy.CanResolve(
            typeof(ConcretDirectStrategyGenericTestCo<IAssignmentStrategy>),
            typeof(IDirectStrategyGenericTestCo<DirectSetValueStrategy>), out _);

        Assert.False(assign);
    }

    [Theory]
    [InlineData(typeof(int), typeof(long), false)]
    [InlineData(typeof(long), typeof(int), false)]
    public void When_Primitives(Type primitiveSourceType, Type primitiveTargetType, bool can)
    {
        var assign = assignmentStrategy.CanResolve(primitiveSourceType, primitiveTargetType, out _);

        Assert.Equal(can, assign);
    }

    [Theory]
    [InlineData(typeof(A), typeof(A), true)]
    [InlineData(typeof(A), typeof(B), false)]
    public void When_Enum(Type enumSourceType, Type enumTargetType, bool can)
    {
        var assign = assignmentStrategy.CanResolve(enumSourceType, enumTargetType, out _);

        Assert.Equal(can, assign);
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
