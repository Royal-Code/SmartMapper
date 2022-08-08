using System;
using FluentAssertions;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;
using RoyalCode.SmartMapper.Infrastructure.Core;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Builders;

public class AdapterSourceToMethodPropertiesOptionsBuilderTests
{
    [Fact]
    public void Parameter_Must_Throw_When_NotIsAProperty()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = new SourceToMethodOptions();
        adapterOptions.AddToMethod(methodOptions);
        
        var builder = new AdapterSourceToMethodPropertiesOptionsBuilder<Foo>(adapterOptions, methodOptions);

        var act = () => builder.Parameter<Delegate>(f => f.DoSomething);

        act.Should().Throw<InvalidPropertySelectorException>();
    }
    
    [Fact]
    public void Parameter_Must_ReturnTheStrategyBuilder()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = new SourceToMethodOptions();
        adapterOptions.AddToMethod(methodOptions);
        
        var builder = new AdapterSourceToMethodPropertiesOptionsBuilder<Foo>(adapterOptions, methodOptions);

        var strategy = builder.Parameter(f => f.Value);
        strategy.Should().NotBeNull();
    }

    [Theory]
    [InlineData("value", true)]
    [InlineData("abc", false)]
    public void Parameter_Must_ValidateTheParameterName(string parameterName, bool isValid)
    {
        var methodOptions = new SourceToMethodOptions()
        {
            Method = typeof(Bar).GetMethod("DoSomething") ?? throw new InvalidOperationException()
        };
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        adapterOptions.AddToMethod(methodOptions);
        
        var builder = new AdapterSourceToMethodPropertiesOptionsBuilder<Foo>(adapterOptions, methodOptions);

        var act = () => builder.Parameter(f => f.Value, parameterName);

        if (isValid)
            act.Should().NotThrow();
        else
            act.Should().Throw<InvalidParameterNameException>();
    }

    [Fact]
    public void Parameter_Must_SetPropertyOptionsWithTheParameterOptions()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = new SourceToMethodOptions();
        adapterOptions.AddToMethod(methodOptions);
        var propertyOptions = adapterOptions.GetPropertyOptions(typeof(Foo).GetProperty(nameof(Foo.Value))!);

        var builder = new AdapterSourceToMethodPropertiesOptionsBuilder<Foo>(adapterOptions, methodOptions);
        builder.Parameter(f => f.Value);

        propertyOptions.ResolutionOptions.Should().BeOfType<PropertyToParameterOptions>();
        propertyOptions.ResolutionStatus.Should().BeOneOf(ResolutionStatus.MappedToMethodParameter);
    }

    [Fact]
    public void Parameter_Must_SetMethodOptionsWithParameterOptions()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = new SourceToMethodOptions();
        adapterOptions.AddToMethod(methodOptions);

        var builder = new AdapterSourceToMethodPropertiesOptionsBuilder<Foo>(adapterOptions, methodOptions);
        builder.Parameter(f => f.Value);

        var found = methodOptions.TryGetPropertyToParameterOptions(
            typeof(Foo).GetProperty(nameof(Foo.Value))!
            , out var propertyToParameterOptions);

        found.Should().BeTrue();
        propertyToParameterOptions.Should().NotBeNull();
    }

    [Fact]
    public void Ignore_Must_Throw_When_NotIsAProperty()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = new SourceToMethodOptions();
        adapterOptions.AddToMethod(methodOptions);
        
        var builder = new AdapterSourceToMethodPropertiesOptionsBuilder<Foo>(adapterOptions, methodOptions);

        var act = () => builder.Ignore<Delegate>(f => f.DoSomething);

        act.Should().Throw<InvalidPropertySelectorException>();
    }

    [Fact]
    public void Ignore_Must_IgnoreThePropertyMapping()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = new SourceToMethodOptions();
        adapterOptions.AddToMethod(methodOptions);
        
        var builder = new AdapterSourceToMethodPropertiesOptionsBuilder<Foo>(adapterOptions, methodOptions);
        
        builder.Ignore(f => f.Value);

        var propertyOptions = adapterOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        propertyOptions.ResolutionStatus.Should().Be(ResolutionStatus.Ignored);
    }
    
    private class Foo
    {
        public string Value { get; set; }
        public void DoSomething(string value) { }
    }
    
    private class Bar
    {
        public void DoSomething(string value) { }
    }
}