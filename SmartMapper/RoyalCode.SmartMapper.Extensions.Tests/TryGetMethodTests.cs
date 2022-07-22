using System;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;

namespace RoyalCode.SmartMapper.Extensions.Tests;

public class TryGetMethodTests
{
    [Fact]
    public void TryGetMethod_With_Valid_Expression_Delegate()
    {
        Expression expression = TestClass.CreateExpression(t => t.DoSomething);
        
        bool result = expression.TryGetMethod(out MethodInfo? methodInfo);
        
        Assert.True(result);
        Assert.NotNull(methodInfo);
    }
    
    public class TestClass
    {
        public string? Value { get; set; }
        
        public void DoSomething(string value)
        {
            Value = value;
        }
        
        public static Expression<Func<TestClass, Delegate>> CreateExpression(Expression<Func<TestClass, Delegate>> exp) => exp;
    }
}