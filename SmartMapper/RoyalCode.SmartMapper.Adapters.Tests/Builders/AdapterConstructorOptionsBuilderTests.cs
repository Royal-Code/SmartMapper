using System;
using FluentAssertions;
using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Builders;

public class AdapterConstructorOptionsBuilderTests
{
    [Fact]
    public void WithParameters_Must_Set_NumberOfParameters()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var constructorOptions = adapterOptions.TargetOptions.GetConstructorOptions();
        var builder = new AdapterConstructorOptionsBuilder<Foo>(adapterOptions, constructorOptions);
        
        builder.WithParameters(10);

        constructorOptions.NumberOfParameters.Should().Be(10);
    }

    [Fact]
    public void WithParameters_Must_Set_ParameterTypes()
    { 
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var constructorOptions = adapterOptions.TargetOptions.GetConstructorOptions();
        var builder = new AdapterConstructorOptionsBuilder<Foo>(adapterOptions, constructorOptions);
        
        var types = new Type[]{ typeof(int), typeof(string) };
        
        builder.WithParameters(types);

        constructorOptions.ParameterTypes.Should().BeEquivalentTo(types);
    }

    [Fact]
    public void Parameters_Must_ExecuteTheAction()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var constructorOptions = adapterOptions.TargetOptions.GetConstructorOptions();
        var builder = new AdapterConstructorOptionsBuilder<Foo>(adapterOptions, constructorOptions);

        IAdapterConstructorParametersOptionsBuilder<Foo>? parametersBuilder = null;
            
        builder.Parameters(b =>
        {
            parametersBuilder = b;
        });
        
        parametersBuilder.Should().NotBeNull();
    }

    private class Foo
    {

    }

    private class Bar
    {

    }
}
