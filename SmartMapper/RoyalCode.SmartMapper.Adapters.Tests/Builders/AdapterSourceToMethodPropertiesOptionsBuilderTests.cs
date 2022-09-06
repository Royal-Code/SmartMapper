using System;
using FluentAssertions;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Core;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Builders;

public class AdapterSourceToMethodPropertiesOptionsBuilderTests
{
    [Fact]
    public void Parameter_Must_Throw_When_NotIsAProperty()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = adapterOptions.CreateSourceToMethodOptions();
        
        var builder = new AdapterSourceToMethodPropertiesOptionsBuilder<Foo>(adapterOptions, methodOptions);

        var act = () => builder.Parameter<Delegate>(f => f.DoSomething);

        act.Should().Throw<InvalidPropertySelectorException>();
    }
    
    [Fact]
    public void Parameter_Must_ReturnTheStrategyBuilder()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = adapterOptions.CreateSourceToMethodOptions();
        
        var builder = new AdapterSourceToMethodPropertiesOptionsBuilder<Foo>(adapterOptions, methodOptions);

        var strategy = builder.Parameter(f => f.Value);
        strategy.Should().NotBeNull();
    }

    [Theory]
    [InlineData("value", true)]
    [InlineData("abc", false)]
    public void Parameter_Must_ValidateTheParameterName(string parameterName, bool isValid)
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = adapterOptions.CreateSourceToMethodOptions();
        methodOptions.MethodOptions.Method = typeof(Bar).GetMethod("DoSomething") 
                                             ?? throw new InvalidOperationException();

        var builder = new AdapterSourceToMethodPropertiesOptionsBuilder<Foo>(adapterOptions, methodOptions);

        var act = () => builder.Parameter(f => f.Value, parameterName);

        if (isValid)
            act.Should().NotThrow();
        else
            act.Should().Throw<InvalidParameterNameException>();
    }

    [Fact]
    public void Parameter_Must_SetPropertyOptions_With_ToMethodParameterOptions()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = adapterOptions.CreateSourceToMethodOptions();
        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(typeof(Foo).GetProperty(nameof(Foo.Value))!);

        var builder = new AdapterSourceToMethodPropertiesOptionsBuilder<Foo>(adapterOptions, methodOptions);
        builder.Parameter(f => f.Value);

        propertyOptions.ResolutionOptions.Should().BeOfType<ToMethodParameterOptions>();
        propertyOptions.ResolutionStatus.Should().BeOneOf(ResolutionStatus.MappedToMethodParameter);
    }

    [Fact]
    public void Parameter_Must_SetMethodOptions_With_ToMethodParameterOptions()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = adapterOptions.CreateSourceToMethodOptions();

        var builder = new AdapterSourceToMethodPropertiesOptionsBuilder<Foo>(adapterOptions, methodOptions);
        builder.Parameter(f => f.Value);

        var found = methodOptions.MethodOptions.TryGetParameterOptions(
            typeof(Foo).GetProperty(nameof(Foo.Value))!
            , out var toMethodParameter);

        found.Should().BeTrue();
        toMethodParameter.Should().NotBeNull();
    }

    [Fact]
    public void Ignore_Must_Throw_When_NotIsAProperty()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = adapterOptions.CreateSourceToMethodOptions();
        
        var builder = new AdapterSourceToMethodPropertiesOptionsBuilder<Foo>(adapterOptions, methodOptions);

        var act = () => builder.Ignore<Delegate>(f => f.DoSomething);

        act.Should().Throw<InvalidPropertySelectorException>();
    }

    [Fact]
    public void Ignore_Must_IgnoreThePropertyMapping()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = adapterOptions.CreateSourceToMethodOptions();
        
        var builder = new AdapterSourceToMethodPropertiesOptionsBuilder<Foo>(adapterOptions, methodOptions);
        
        builder.Ignore(f => f.Value);

        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        propertyOptions.ResolutionStatus.Should().Be(ResolutionStatus.Ignored);
    }
    
#pragma warning disable CS8618

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