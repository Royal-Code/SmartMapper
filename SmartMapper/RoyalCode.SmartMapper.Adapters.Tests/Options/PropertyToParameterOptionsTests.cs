using System;
using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Options;

public class PropertyToParameterOptionsTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void UseParameterName_Must_NotBeNullOrEmpty(string parameterName)
    {
        var methodOptions = new AdapterSourceToMethodOptions();
        var propertyInfo = typeof(Foo).GetProperty(nameof(Foo.Value))!;
        
        var options = new PropertyToParameterOptions(methodOptions, propertyInfo);
        
        Assert.Throws<ArgumentException>(() => options.UseParameterName(parameterName));
    }
    
    [Fact]
    public void UseParameterName_Must_AcceptAnyParameterName_When_MethodNotSet()
    {
        var methodOptions = new AdapterSourceToMethodOptions();
        var propertyInfo = typeof(Foo).GetProperty(nameof(Foo.Value))!;
        
        var options = new PropertyToParameterOptions(methodOptions, propertyInfo);
        
        options.UseParameterName("abc");
    }
    
    [Theory]
    [InlineData("value", true)]
    [InlineData("abc", false)]
    public void UseParameterName_Must_ValidateParameterName_When_MethodSet(string parameterName, bool isValid)
    {
        var methodOptions = new AdapterSourceToMethodOptions
        {
            Method = typeof(Bar).GetMethod("DoSomething") ?? throw new InvalidOperationException()
        };
        var propertyInfo = typeof(Foo).GetProperty(nameof(Foo.Value))!;
        var options = new PropertyToParameterOptions(methodOptions, propertyInfo);
        
        var act = () => options.UseParameterName(parameterName);
        
        if (isValid)
            act.Should().NotThrow();
        else
            act.Should().Throw<ArgumentException>();
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