using System;
using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Options;

public class AdapterOptionsTests
{
    [Fact]
    public void GetSourceToMethodOptions_Must_ReturnNull_When_NoOptions()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        
        var result = options.GetSourceToMethodOptions();
        
        result.Should().NotBeNull().And.BeEmpty();
    }
    
    [Fact]
    public void GetSourceToMethodOptions_Must_ReturnOptions_When_Options()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        options.AddToMethod(new AdapterSourceToMethodOptions());
        options.AddToMethod(new AdapterSourceToMethodOptions());
        options.AddToMethod(new AdapterSourceToMethodOptions());
        
        var result = options.GetSourceToMethodOptions();
        
        result.Should().NotBeNull().And.HaveCount(3);
    }

    [Fact]
    public void GetPropertyOptions_MustThrow_When_PropertyNotFromSourceType()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        
        Action action = () => options.GetPropertyOptions(typeof(Bar).GetProperty("Value")!);
        
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GetPropertyOptions_Must_Return_NewOptions()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        
        var result = options.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        
        result.Should().NotBeNull();
        result.Property.Should().NotBeNull();
        result.Property.Name.Should().Be("Value");
    }

    [Fact]
    public void GetPropertyOptions_Must_Return_SameInstances()
    {
            var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        
        var result1 = options.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var result2 = options.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        
        result1.Should().BeSameAs(result2);
    }

    private class Foo
    {
        public string Value { get; set; }
    }

    private class Bar
    {
        public string Value { get; set; }
    }
}