using System;
using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Options;

public class SourceOptionsTests
{
    [Fact]
    public void GetSourceToMethodOptions_Must_ReturnNull_When_NoOptions()
    {
        var options = new SourceOptions(typeof(Foo));
        
        var result = options.GetSourceToMethodOptions();
        
        result.Should().NotBeNull().And.BeEmpty();
    }
    
    [Fact]
    public void GetPropertyOptions_MustThrow_When_PropertyNotFromSourceType()
    {
        var options = new SourceOptions(typeof(Foo));
        
        Action action = () => options.GetPropertyOptions(typeof(Bar).GetProperty("Value")!);
        
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GetPropertyOptions_Must_AcceptPropertiesFromInheritTypes()
    {
        var options = new SourceOptions(typeof(Foo));

        var result = options.GetPropertyOptions(typeof(Quux).GetProperty("Other")!);

        result.Should().NotBeNull();
        result.Property.Should().NotBeNull();
        result.Property.Name.Should().Be("Other");
    }

    [Fact]
    public void GetPropertyOptions_Must_Return_NewOptions()
    {
        var options = new SourceOptions(typeof(Foo));
        
        var result = options.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        
        result.Should().NotBeNull();
        result.Property.Should().NotBeNull();
        result.Property.Name.Should().Be("Value");
    }

    [Fact]
    public void GetPropertyOptions_Must_Return_SameInstances()
    {
        var options = new SourceOptions(typeof(Foo));
        
        var result1 = options.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var result2 = options.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        
        result1.Should().BeSameAs(result2);
    }
    
    [Theory]
    [InlineData("InvalidName", false)]
    [InlineData("Value", true)]
    [InlineData("Other", true)]
    public void TryGetPropertyOptions_Must_Return_True_When_PropertyExists(string propertyName, bool expected)
    {
        var options = new SourceOptions(typeof(Foo));
        
        var result = options.TryGetPropertyOptions(propertyName, out var propertyOptions);
        
        result.Should().Be(expected);
        if (expected)
        {
            propertyOptions.Should().NotBeNull();
            propertyOptions!.Property.Should().NotBeNull();
            propertyOptions.Property.Name.Should().Be(propertyName);
        }
        else
        {
            propertyOptions.Should().BeNull();
        }
    }
    
#pragma warning disable CS8618
    
    private class Foo : Quux
    {
        public string Value { get; set; }
    }
    
    private class Quux
    {
        public string Other { get; set; }
    }
    
    private class Bar
    {
        public string Value { get; set; }
    }
}