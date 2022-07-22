using System;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;

namespace RoyalCode.SmartMapper.Extensions.Tests;

public class TryGetMemberTest
{
    [Fact]
    public void TryGetMember_With_Valid_Expression_MemberAccess()
    {
        Expression expression = TestClass.CreateExpression(t => t.TestProperty);

        var result = expression.TryGetMember(out MemberInfo? member);

        Assert.True(result);
        Assert.NotNull(member);
    }
    
    private class TestClass
    {
        public string? TestProperty { get; set; }

        public static Expression<Func<TestClass, T>> CreateExpression<T>(Expression<Func<TestClass, T>> exp) => exp;
    }
}

