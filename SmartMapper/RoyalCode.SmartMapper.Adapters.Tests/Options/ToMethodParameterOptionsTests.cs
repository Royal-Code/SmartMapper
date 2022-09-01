using System;
using FluentAssertions;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Options;

public class ToMethodParameterOptionsTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void UseParameterName_Must_NotBeNullOrEmpty(string parameterName)
    {
        var methodOptions = new MethodOptions(typeof(Bar));
        var propertyInfo = typeof(Foo).GetProperty(nameof(Foo.Value))!;
        
        var options = new ToMethodParameterOptions(methodOptions, propertyInfo);
        
        Assert.Throws<InvalidParameterNameException>(() => options.UseParameterName(parameterName));
    }
    
    [Fact]
    public void UseParameterName_Must_AcceptAnyParameterName_When_MethodNotSet()
    {
        var methodOptions = new MethodOptions(typeof(Bar));
        var propertyInfo = typeof(Foo).GetProperty(nameof(Foo.Value))!;
        
        var options = new ToMethodParameterOptions(methodOptions, propertyInfo);
        
        options.UseParameterName("abc");
    }
    
    [Theory]
    [InlineData("value", true)]
    [InlineData("abc", false)]
    public void UseParameterName_Must_ValidateParameterName_When_MethodSet(string parameterName, bool isValid)
    {
        var methodOptions = new MethodOptions(typeof(Bar))
        {
            Method = typeof(Bar).GetMethod("DoSomething") ?? throw new InvalidOperationException()
        };
        var propertyInfo = typeof(Foo).GetProperty(nameof(Foo.Value))!;
        var options = new ToMethodParameterOptions(methodOptions, propertyInfo);
        
        var act = () => options.UseParameterName(parameterName);
        
        if (isValid)
            act.Should().NotThrow();
        else
            act.Should().Throw<InvalidParameterNameException>();
    }
    
    private class Foo
    {
        public string Value { get; set; }
    }
    
    private class Bar
    {
        public void DoSomething(string value){ }
    }
}