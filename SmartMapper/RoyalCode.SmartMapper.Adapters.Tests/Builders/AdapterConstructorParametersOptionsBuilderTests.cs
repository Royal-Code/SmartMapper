using FluentAssertions;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;
using RoyalCode.SmartMapper.Infrastructure.Core;
using System;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Builders;

public class AdapterConstructorParametersOptionsBuilderTests
{
    [Fact]
    public void Parameter_Must_Throw_When_NotIsAProperty()
    {
        // Arrange
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var constructorOptions = adapterOptions.GetConstructorOptions();

        var builder = new AdapterConstructorParametersOptionsBuilder<Foo>(adapterOptions, constructorOptions);

        // Act
        var act = () => builder.Parameter<Delegate>(f => f.DoSomething);

        // Assert
        act.Should().Throw<InvalidPropertySelectorException>();
    }

    [Fact]
    public void Parameter_Must_ReturnTheStrategyBuilder()
    {
        // Arrange
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var constructorOptions = adapterOptions.GetConstructorOptions();

        var builder = new AdapterConstructorParametersOptionsBuilder<Foo>(adapterOptions, constructorOptions);
        
        // Act
        var strategyBuilder = builder.Parameter(f => f.Value);
        
        // Assert
        strategyBuilder.Should().NotBeNull();
    }

    [Theory]
    [InlineData("value", true)]
    [InlineData("abc", false)]
    public void Parameter_Must_ValidateTheParameterName(string parameterName, bool isValid)
    {
        // Arrange
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var constructorOptions = adapterOptions.GetConstructorOptions();

        var builder = new AdapterConstructorParametersOptionsBuilder<Foo>(adapterOptions, constructorOptions);

        // Act
        var act = () => builder.Parameter(f => f.Value, parameterName);

        // Assert
        if (isValid)
        {
            act.Should().NotThrow();
        }
        else
        {
            act.Should().Throw<InvalidParameterNameException>();
        }
    }

    [Fact]
    public void Parameter_Must_SetPropertyOptions_With_ConstructorParameterOptions()
    {
        // Arrange
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var constructorOptions = adapterOptions.GetConstructorOptions();
        var propertyOptions = adapterOptions.GetPropertyOptions(typeof(Foo).GetProperty(nameof(Foo.Value))!);

        // Act
        var builder = new AdapterConstructorParametersOptionsBuilder<Foo>(adapterOptions, constructorOptions);
        builder.Parameter(f => f.Value);

        // Assert
        propertyOptions.ResolutionOptions.Should().BeOfType<ConstructorParameterOptions>();
        propertyOptions.ResolutionStatus.Should().BeOneOf(ResolutionStatus.MappedToConstructorParameter);
    }

    [Fact]
    public void Parameter_Must_SetConstructorOptions_With_TheConstructorParameterOptions()
    {
        // Arrange
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var constructorOptions = adapterOptions.GetConstructorOptions();

        var builder = new AdapterConstructorParametersOptionsBuilder<Foo>(adapterOptions, constructorOptions);
        builder.Parameter(f => f.Value);

        // Act
        var configured = constructorOptions.TryGetConstructorParameterOptions(
            typeof(Foo).GetProperty(nameof(Foo.Value))!, 
            out var configuredOptions);
        
        // Assert
        configured.Should().BeTrue();
        configuredOptions.Should().NotBeNull();
    }
    
    [Fact]
    public void Parameter_Must_SetParameterName_To_ConstructorParameterOptions()
    {
        // Arrange
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var constructorOptions = adapterOptions.GetConstructorOptions();

        var builder = new AdapterConstructorParametersOptionsBuilder<Foo>(adapterOptions, constructorOptions);
        builder.Parameter(f => f.Value, "value");

        // Act
        var configured = constructorOptions.TryGetConstructorParameterOptions(
            typeof(Foo).GetProperty(nameof(Foo.Value))!, 
            out var configuredOptions);
        
        // Assert
        configured.Should().BeTrue();
        configuredOptions.Should().NotBeNull();
        configuredOptions!.ParameterName.Should().Be("value");
    }

    private class Foo
    {
        public string Value { get; }
        public void DoSomething(string value) { }
    }

    private class Bar 
    {
        public Bar(string value)
        {

        }
    }
}