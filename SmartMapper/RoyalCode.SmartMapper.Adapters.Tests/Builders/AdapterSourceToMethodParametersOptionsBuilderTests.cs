using System;
using FluentAssertions;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Core;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Builders;

public class AdapterSourceToMethodParametersOptionsBuilderTests
{
    [Fact]
    public void Parameter_Must_Throw_When_NotIsAProperty()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = adapterOptions.CreateSourceToMethodOptions();
        
        var builder = new AdapterSourceToMethodParametersOptionsBuilder<Foo>(adapterOptions, methodOptions);

        var act = () => builder.Parameter<Delegate>(f => f.DoSomething);

        act.Should().Throw<InvalidPropertySelectorException>();
    }
    
    [Fact]
    public void Parameter_Must_ReturnTheStrategyBuilder()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = adapterOptions.CreateSourceToMethodOptions();
        methodOptions.Strategy = SourceToMethodStrategy.SelectedParameters;
        
        var builder = new AdapterSourceToMethodParametersOptionsBuilder<Foo>(adapterOptions, methodOptions);
        var strategyBuilder = builder.Parameter(f => f.Value);
        strategyBuilder.Should().NotBeNull();
    }

    [Fact]
    public void Parameter_Must_SetPropertyOptionsWithTheParameterOptions()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = adapterOptions.CreateSourceToMethodOptions();
        methodOptions.Strategy = SourceToMethodStrategy.SelectedParameters;
        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(typeof(Foo).GetProperty(nameof(Foo.Value))!);

        var builder = new AdapterSourceToMethodParametersOptionsBuilder<Foo>(adapterOptions, methodOptions);
        builder.Parameter(f => f.Value);

        propertyOptions.ResolutionOptions.Should().BeOfType<ToMethodParameterOptions>();
        propertyOptions.ResolutionStatus.Should().BeOneOf(ResolutionStatus.MappedToMethodParameter);
    }

    [Fact]
    public void Parameter_Must_AddTheParameterToTheMethodOptionsSequence()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = adapterOptions.CreateSourceToMethodOptions();
        methodOptions.Strategy = SourceToMethodStrategy.SelectedParameters;
        
        methodOptions.GetAllParameterSequence().Should().BeEmpty();
        
        var builder = new AdapterSourceToMethodParametersOptionsBuilder<Foo>(adapterOptions, methodOptions);
        builder.Parameter(f => f.Value);

        methodOptions.GetAllParameterSequence().Should().HaveCount(1);
    }
    
#pragma warning disable CS8618

    private class Foo
    {
        public string Value { get; }
        public void DoSomething(string value) { }
    }
    
    private class Bar { }
}