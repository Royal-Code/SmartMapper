
using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Resolvers;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Parameters;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Resolvers;

public class ParameterResolverTests
{
    [Fact]
    public void Resolve_Must_ResolveSingleParameter_ForSameNameProperty()
    {
        // arrange
        var configs = ConfigurationBuilder.CreateDefault().Build();
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var adapterContext = new AdapterContext(adapterOptions, configs);
        var activationRequest = adapterContext.CreateActivationRequest();
        var activationContext = activationRequest.CreateContext();
        var elegibleConstructor = adapterContext.CreateEligibleConstructors().First();
        var constructorRequest = elegibleConstructor.CreateConstructorRequest(activationContext);
        var constructorContext = constructorRequest.CreateContext();
        var parameterRequest = constructorContext.ConstrutorParameterRequests().First();
        
        var resolver = new ParameterResolver();

        // act
        var found = resolver.TryResolve(parameterRequest, out var resolution);

        // assert
        found.Should().BeTrue();
        resolution.Should().NotBeNull();
        resolution!.Resolved.Should().BeTrue();
    }

    [Fact]
    public void Resolve_Must_Not_ResolveSingleParameter_ForOtherNameProperty()
    {
        // arrange
        var configs = ConfigurationBuilder.CreateDefault().Build();
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Baz));
        var adapterContext = new AdapterContext(adapterOptions, configs);
        var activationRequest = adapterContext.CreateActivationRequest();
        var activationContext = activationRequest.CreateContext();
        var elegibleConstructor = adapterContext.CreateEligibleConstructors().First();
        var constructorRequest = elegibleConstructor.CreateConstructorRequest(activationContext);
        var constructorContext = constructorRequest.CreateContext();
        var parameterRequest = constructorContext.ConstrutorParameterRequests().First();

        var resolver = new ParameterResolver();

        // act
        var found = resolver.TryResolve(parameterRequest, out var resolution);

        // assert
        found.Should().BeFalse();
        resolution.Should().BeNull();
    }

    [Fact]
    public void Resolve_Must_ResolveSingleParameter_ForOtherNameProperty_WithParameterOptionsName_Configured()
    {
        // arrange
        var configs = ConfigurationBuilder.CreateDefault().Build();
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Baz));
        var adapterContext = new AdapterContext(adapterOptions, configs);
        var activationRequest = adapterContext.CreateActivationRequest();
        var activationContext = activationRequest.CreateContext();
        var elegibleConstructor = adapterContext.CreateEligibleConstructors().First();
        var constructorRequest = elegibleConstructor.CreateConstructorRequest(activationContext);
        var constructorContext = constructorRequest.CreateContext();
        var parameterRequest = constructorContext.ConstrutorParameterRequests().First();

        var builder = new AdapterPropertyToConstructorParametersOptionsBuilder<Foo>(
            adapterOptions.SourceOptions,
            adapterOptions.TargetOptions.GetConstructorOptions());
        builder.Parameter(f => f.Value, "other");

        var resolver = new ParameterResolver();

        // act
        var found = resolver.TryResolve(parameterRequest, out var resolution);

        // assert
        found.Should().BeTrue();
        resolution.Should().NotBeNull();
        resolution!.Resolved.Should().BeTrue();
    }

    [Fact]
    public void Resolve_Must_Not_ResolveSingleParameter_ForOtherNameProperty_WithParameterOptionsName_Not_Configured()
    {
        // arrange
        var configs = ConfigurationBuilder.CreateDefault().Build();
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Baz));
        var adapterContext = new AdapterContext(adapterOptions, configs);
        var activationRequest = adapterContext.CreateActivationRequest();
        var activationContext = activationRequest.CreateContext();
        var elegibleConstructor = adapterContext.CreateEligibleConstructors().First();
        var constructorRequest = elegibleConstructor.CreateConstructorRequest(activationContext);
        var constructorContext = constructorRequest.CreateContext();
        var parameterRequest = constructorContext.ConstrutorParameterRequests().First();

        var builder = new AdapterPropertyToConstructorParametersOptionsBuilder<Foo>(
            adapterOptions.SourceOptions,
            adapterOptions.TargetOptions.GetConstructorOptions());
        builder.Parameter(f => f.Value);

        var resolver = new ParameterResolver();

        // act
        var found = resolver.TryResolve(parameterRequest, out var resolution);

        // assert
        found.Should().BeFalse();
        resolution.Should().BeNull();
    }
}

file class Foo
{
    public string Value { get; set; }
}

file class Bar
{
    public Bar(string value)
    {
        Value = value;
    }

    public string Value { get; }
}

file class Baz
{
    public Baz(string other)
    {
        Value = other;
    }

    public string Value { get; }
}