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
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var constructorOptions = adapterOptions.GetConstructorOptions();

        var builder = new AdapterConstructorParametersOptionsBuilder<Foo>(adapterOptions, constructorOptions);

        var act = () => builder.Parameter<Delegate>(f => f.DoSomething);

        act.Should().Throw<InvalidPropertySelectorException>();
    }

    [Fact]
    public void Parameter_Must_ReturnTheStrategyBuilder()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var constructorOptions = adapterOptions.GetConstructorOptions();

        var builder = new AdapterConstructorParametersOptionsBuilder<Foo>(adapterOptions, constructorOptions);
        var strategyBuilder = builder.Parameter(f => f.Value);
        strategyBuilder.Should().NotBeNull();
    }

    [Theory]
    [InlineData("value", true)]
    [InlineData("abc", false)]
    public void Parameter_Must_ValidateTheParameterName(string parameterName, bool isValid)
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var constructorOptions = adapterOptions.GetConstructorOptions();

        var builder = new AdapterConstructorParametersOptionsBuilder<Foo>(adapterOptions, constructorOptions);

        var act = () => builder.Parameter(f => f.Value, parameterName);

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
    public void Parameter_Must_SetPropertyOptionsWithTheConstructorParameterOptions()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var constructorOptions = adapterOptions.GetConstructorOptions();
        var propertyOptions = adapterOptions.GetPropertyOptions(typeof(Foo).GetProperty(nameof(Foo.Value))!);

        var builder = new AdapterConstructorParametersOptionsBuilder<Foo>(adapterOptions, constructorOptions);
        builder.Parameter(f => f.Value);

        propertyOptions.ResolutionOptions.Should().BeOfType<PropertyToConstructorOptions>();
        propertyOptions.ResolutionStatus.Should().BeOneOf(ResolutionStatus.MappedToConstructor);
    }

    [Fact]
    public void Parameter_Must_SetConstructorOptionsWithTheConstructorParameterOptions()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var constructorOptions = adapterOptions.GetConstructorOptions();

        var builder = new AdapterConstructorParametersOptionsBuilder<Foo>(adapterOptions, constructorOptions);
        builder.Parameter(f => f.Value);

        // criar as verificações
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