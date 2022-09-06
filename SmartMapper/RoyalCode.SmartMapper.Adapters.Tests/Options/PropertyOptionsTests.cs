using System;
using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Options;

public class PropertyOptionsTests
{
    [Fact]
    public void MappedToMethodParameter_Must_SetResolvedProperty()
    {
        var methodOptions = new MethodOptions(typeof(Bar));
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var propertyToParameterOptions = new ToMethodParameterOptions(methodOptions, propertyInfo);
        var options = new PropertyOptions(propertyInfo);
        
        options.MappedToMethodParameter(propertyToParameterOptions);

        propertyToParameterOptions.ResolvedProperty.Should().NotBeNull().And.BeSameAs(options);
    }

    [Fact]
    public void MappedToMethodParameter_Must_SetResolutionOptions()
    {
        var methodOptions = new MethodOptions(typeof(Bar));
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var propertyToParameterOptions = new ToMethodParameterOptions(methodOptions, propertyInfo);
        var options = new PropertyOptions(propertyInfo);
        
        options.MappedToMethodParameter(propertyToParameterOptions);

        options.ResolutionOptions.Should().NotBeNull().And.BeSameAs(propertyToParameterOptions);
    }
    
    [Fact]
    public void MappedToMethodParameter_Must_Throw_When_ResolutionStatusIsSetPreviously()
    {
        var methodOptions = new MethodOptions(typeof(Bar));
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var propertyToParameterOptions = new ToMethodParameterOptions(methodOptions, propertyInfo);
        var options = new PropertyOptions(propertyInfo);
        
        options.IgnoreMapping();
        
        var action = () => options.MappedToMethodParameter(propertyToParameterOptions);
        
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void MappedToConstructorParameter_Must_SetResolvedProperty()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var toConstructorParameter = new ToConstructorParameterOptions(typeof(Bar), propertyInfo);
        var options = new PropertyOptions(propertyInfo);

        options.MappedToConstructorParameter(toConstructorParameter);

        toConstructorParameter.ResolvedProperty.Should().NotBeNull().And.BeSameAs(options);
    }

    [Fact]
    public void MappedToConstructorParameter_Must_SetResolutionOptions()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var toConstructorParameter = new ToConstructorParameterOptions(typeof(Bar), propertyInfo);
        var options = new PropertyOptions(propertyInfo);

        options.MappedToConstructorParameter(toConstructorParameter);

        options.ResolutionOptions.Should().NotBeNull().And.BeSameAs(toConstructorParameter);
    }

    [Fact]
    public void MappedToConstructorParameter_Must_Throw_When_ResolutionStatusIsSetPreviously()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var toConstructorParameter = new ToConstructorParameterOptions(typeof(Bar), propertyInfo);
        var options = new PropertyOptions(propertyInfo);
        
        options.IgnoreMapping();

        var action = () => options.MappedToConstructorParameter(toConstructorParameter);

        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void MappedToProperty_Must_SetResolvedProperty()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);

        var propertyToProperty = new ToPropertyOptions(typeof(Bar), typeof(Bar).GetProperty("Value")!);

        options.MappedToProperty(propertyToProperty);
        
        propertyToProperty.ResolvedProperty.Should().NotBeNull().And.BeSameAs(options);
    }

    [Fact]
    public void MappedToProperty_Must_SetResolutionOptions()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);

        var propertyToProperty = new ToPropertyOptions(typeof(Bar), typeof(Bar).GetProperty("Value")!);

        options.MappedToProperty(propertyToProperty);

        options.ResolutionOptions.Should().NotBeNull().And.BeSameAs(propertyToProperty);
    }

    [Fact]
    public void MappedToProperty_Must_Throw_When_ResolutionStatusIsSetPreviously()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);
        options.IgnoreMapping();
        
        var propertyToProperty = new ToPropertyOptions(typeof(Bar), typeof(Bar).GetProperty("Value")!);

        var action = () => options.MappedToProperty(propertyToProperty);
        
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void IgnoreMapping_Must_Throw_When_ResolutionStatusIsSetPreviously()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);
        var propertyToProperty = new ToPropertyOptions(typeof(Bar), typeof(Bar).GetProperty("Value")!);
        
        options.MappedToProperty(propertyToProperty);
        
        var action = () => options.IgnoreMapping();
        
        action.Should().Throw<InvalidOperationException>();
    }
    
    // TODO: test for other methods (MappedToMethod, MappedToConstructor, ThenMapTo)
    
#pragma warning disable CS8618

    private class Foo
    {
        public string Value { get; set; }
    }

    private class Bar { }
}