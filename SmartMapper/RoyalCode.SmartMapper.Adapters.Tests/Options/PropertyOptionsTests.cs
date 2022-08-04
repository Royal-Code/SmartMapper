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
        
        var methodOptions = new AdapterSourceToMethodOptions();
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

        var methodOptions = new AdapterSourceToMethodOptions();
        var propertyToParameterOptions = new PropertyToParameterOptions(methodOptions, propertyInfo);
        
        options.MappedToMethodParameter(propertyToParameterOptions);

        propertyToParameterOptions.PropertyRelated.Should().NotBeNull();
    }

    [Fact]
    public void MappedToMethodParameter_Must_SetResolutionOptions()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);

        var methodOptions = new AdapterSourceToMethodOptions();
        var propertyToParameterOptions = new PropertyToParameterOptions(methodOptions, propertyInfo);
        
        options.MappedToMethodParameter(propertyToParameterOptions);

        options.ResolutionOptions.Should().NotBeNull();
    }
    
    [Fact]
    public void MappedToMethodParameter_Must_Throw_When_ResolutionStatusIsSetPreviously()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);
        options.IgnoreMapping();
        
        var methodOptions = new AdapterSourceToMethodOptions();
        var propertyToParameterOptions = new PropertyToParameterOptions(methodOptions, propertyInfo);
        
        var action = () => options.MappedToMethodParameter(propertyToParameterOptions);
        
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void MappedToConstructor_Must_SetPropertyRelated()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);

        var propertyToContructor = new PropertyToConstructorOptions(typeof(Bar), propertyInfo);

        options.MappedToConstructor(propertyToContructor);

        propertyToContructor.PropertyRelated.Should().NotBeNull();
    }

    [Fact]
    public void MappedToConstructor_Must_SetResolutionOptions()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);

        var propertyToContructor = new PropertyToConstructorOptions(typeof(Bar), propertyInfo);

        options.MappedToConstructor(propertyToContructor);

        options.ResolutionOptions.Should().NotBeNull();
    }

    [Fact]
    public void MappedToConstructor_Must_Throw_When_ResolutionStatusIsSetPreviously()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);
        options.IgnoreMapping();

        var propertyToContructor = new PropertyToConstructorOptions(typeof(Bar), propertyInfo);

        var action = () => options.MappedToConstructor(propertyToContructor);

        action.Should().Throw<InvalidOperationException>();
    }


    [Fact]
    public void IgnoreMapping_Must_Throw_When_ResolutionStatusIsSetPreviously()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);
        var methodOptions = new AdapterSourceToMethodOptions();
        var propertyToParameterOptions = new PropertyToParameterOptions(methodOptions, propertyInfo);
        
        options.MappedToMethodParameter(propertyToParameterOptions);
        
        var action = () => options.IgnoreMapping();
        
        action.Should().Throw<InvalidOperationException>();
    }
    
    private class Foo
    {
        public string Value { get; set; }
    }

    private class Bar { }
}