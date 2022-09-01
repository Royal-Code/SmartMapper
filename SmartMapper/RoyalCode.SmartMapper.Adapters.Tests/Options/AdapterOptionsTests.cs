using System;
using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Options;

public class AdapterOptionsTests
{
    [Fact]
    public void GetSourceToMethodOptions_Must_ReturnNull_When_NoOptions()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        
        var result = options.SourceOptions.GetSourceToMethodOptions();
        
        result.Should().NotBeNull().And.BeEmpty();
    }
    
    [Fact]
    public void GetSourceToMethodOptions_Must_ReturnOptions_When_Options()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        options.CreateSourceToMethodOptions();
        options.CreateSourceToMethodOptions();
        options.CreateSourceToMethodOptions();
        
        var result = options.GetSourceToMethodOptions();
        
        result.Should().NotBeNull().And.HaveCount(3);
    }

    [Fact]
    public void GetPropertyOptions_MustThrow_When_PropertyNotFromSourceType()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        
        Action action = () => options.SourceOptions.GetPropertyOptions(typeof(Bar).GetProperty("Value")!);
        
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GetPropertyOptions_Must_AcceptPropertiesFromInheritTypes()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));

        var result = options.SourceOptions.GetPropertyOptions(typeof(Quux).GetProperty("Other")!);

        result.Should().NotBeNull();
        result.Property.Should().NotBeNull();
        result.Property.Name.Should().Be("Other");
    }

    [Fact]
    public void GetPropertyOptions_Must_Return_NewOptions()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        
        var result = options.SourceOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        
        result.Should().NotBeNull();
        result.Property.Should().NotBeNull();
        result.Property.Name.Should().Be("Value");
    }

    [Fact]
    public void GetPropertyOptions_Must_Return_SameInstances()
    {
            var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        
        var result1 = options.SourceOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var result2 = options.SourceOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        
        result1.Should().BeSameAs(result2);
    }
    
    [Fact]
    public void GetConstructorOptions_Must_Return_SameInstances()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        
        var result1 = options.TargetOptions.GetConstructorOptions();
        var result2 = options.TargetOptions.GetConstructorOptions();
        
        result1.Should().NotBeNull().And.BeSameAs(result2);
    }
    
    [Theory]
    [InlineData("InvalidName", false)]
    [InlineData("Value", true)]
    [InlineData("Other", true)]
    public void TryGetPropertyOptions_Must_Return_True_When_PropertyExists(string propertyName, bool expected)
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        
        var result = options.SourceOptions.TryGetPropertyOptions(propertyName, out var propertyOptions);
        
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
    
    private class Foo : Quux
    {
        public string Value { get; set; }
    }

    private class Bar
    {
        public string Value { get; set; }
    }

    private class Quux
    {
        public string Other { get; set; }
    }
}