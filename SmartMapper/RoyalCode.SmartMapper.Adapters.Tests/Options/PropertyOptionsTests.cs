using System;
using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Core;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Options;

public class PropertyOptionsTests
{
    [Fact]
    public void ResetMapping_Must_Reset_OptionsValues()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);
        
        var methodOptions = new SourceToMethodOptions();
        var propertyToParameterOptions = new PropertyToParameterOptions(methodOptions, propertyInfo);
        options.MappedToMethodParameter(propertyToParameterOptions);
        options.GetOrCreateAssignmentStrategyOptions<string>();
        
        options.ResetMapping();
        options.ResolutionStatus.Should().Be(ResolutionStatus.Undefined);
        options.AssignmentStrategy.Should().BeNull();
        options.ResolutionOptions.Should().BeNull();
    }
    
    [Fact]
    public void MappedToMethodParameter_Must_SetPropertyRelated()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);

        var methodOptions = new SourceToMethodOptions();
        var propertyToParameterOptions = new PropertyToParameterOptions(methodOptions, propertyInfo);
        
        options.MappedToMethodParameter(propertyToParameterOptions);

        propertyToParameterOptions.PropertyRelated.Should().NotBeNull().And.BeSameAs(options);
    }

    [Fact]
    public void MappedToMethodParameter_Must_SetResolutionOptions()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);

        var methodOptions = new SourceToMethodOptions();
        var propertyToParameterOptions = new PropertyToParameterOptions(methodOptions, propertyInfo);
        
        options.MappedToMethodParameter(propertyToParameterOptions);

        options.ResolutionOptions.Should().NotBeNull().And.BeSameAs(propertyToParameterOptions);
    }
    
    [Fact]
    public void MappedToMethodParameter_Must_Throw_When_ResolutionStatusIsSetPreviously()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);
        options.IgnoreMapping();
        
        var methodOptions = new SourceToMethodOptions();
        var propertyToParameterOptions = new PropertyToParameterOptions(methodOptions, propertyInfo);
        
        var action = () => options.MappedToMethodParameter(propertyToParameterOptions);
        
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void MappedToConstructorParameter_Must_SetPropertyRelated()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);

        var propertyToContructor = new ConstructorParameterOptions(typeof(Bar), propertyInfo);

        options.MappedToConstructorParameter(propertyToContructor);

        propertyToContructor.PropertyRelated.Should().NotBeNull().And.BeSameAs(options);
    }

    [Fact]
    public void MappedToConstructorParameter_Must_SetResolutionOptions()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);

        var propertyToContructor = new ConstructorParameterOptions(typeof(Bar), propertyInfo);

        options.MappedToConstructorParameter(propertyToContructor);

        options.ResolutionOptions.Should().NotBeNull().And.BeSameAs(propertyToContructor);
    }

    [Fact]
    public void MappedToConstructorParameter_Must_Throw_When_ResolutionStatusIsSetPreviously()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);
        options.IgnoreMapping();

        var propertyToContructor = new ConstructorParameterOptions(typeof(Bar), propertyInfo);

        var action = () => options.MappedToConstructorParameter(propertyToContructor);

        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void MappedToProperty_Must_SetPropertyRelated()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);

        var propertyToProperty = new PropertyToPropertyOptions(typeof(Bar), typeof(Bar).GetProperty("Value")!);

        options.MappedToProperty(propertyToProperty);
        
        propertyToProperty.PropertyRelated.Should().NotBeNull().And.BeSameAs(options);
    }

    [Fact]
    public void MappedToProperty_Must_SetResolutionOptions()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);

        var propertyToProperty = new PropertyToPropertyOptions(typeof(Bar), typeof(Bar).GetProperty("Value")!);

        options.MappedToProperty(propertyToProperty);

        options.ResolutionOptions.Should().NotBeNull().And.BeSameAs(propertyToProperty);
    }

    [Fact]
    public void MappedToProperty_Must_Throw_When_ResolutionStatusIsSetPreviously()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);
        options.IgnoreMapping();
        
        var propertyToProperty = new PropertyToPropertyOptions(typeof(Bar), typeof(Bar).GetProperty("Value")!);

        var action = () => options.MappedToProperty(propertyToProperty);
        
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void IgnoreMapping_Must_Throw_When_ResolutionStatusIsSetPreviously()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);
        var methodOptions = new SourceToMethodOptions();
        var propertyToParameterOptions = new PropertyToParameterOptions(methodOptions, propertyInfo);
        
        options.MappedToMethodParameter(propertyToParameterOptions);
        
        var action = () => options.IgnoreMapping();
        
        action.Should().Throw<InvalidOperationException>();
    }
    
    // TODO: test for other methods (MappedToMethod, MappedToConstructor)
    
    private class Foo
    {
        public string Value { get; set; }
    }

    private class Bar { }
}