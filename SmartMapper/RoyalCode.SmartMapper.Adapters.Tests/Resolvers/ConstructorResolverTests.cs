using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Resolvers;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Constructors;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Resolvers;

public class ConstructorResolverTests
{
    [Fact]
    public void Resolve_Must_CreateSuccessResolution_ForDefaultContructorClass()
    {
        // arrange
        var configs = ConfigurationBuilder.CreateDefault().Build();
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var adapterContext = new AdapterContext(adapterOptions, configs);
        var activationRequest = adapterContext.CreateActivationRequest();
        var activationContext = activationRequest.CreateContext();
        var elegibleConstructor = adapterContext.CreateEligibleConstructors().First();
        var constructorRequest = elegibleConstructor.CreateConstructorRequest(activationContext);

        var resolver = new ConstructorResolver();

        // act
        var resolution = resolver.Resolve(constructorRequest);

        // assert
        resolution.Should().NotBeNull();
        resolution.Resolved.Should().BeTrue();
    }

    [Fact]
    public void Resolve_Must_CreateSuccessResolution_ForContructorWithOneParameterAndSamePropertyName()
    {
        // arrange
        var configs = ConfigurationBuilder.CreateDefault().Build();
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Baz));
        var adapterContext = new AdapterContext(adapterOptions, configs);
        var activationRequest = adapterContext.CreateActivationRequest();
        var activationContext = activationRequest.CreateContext();
        var elegibleConstructor = adapterContext.CreateEligibleConstructors().First();
        var constructorRequest = elegibleConstructor.CreateConstructorRequest(activationContext);

        var resolver = new ConstructorResolver();

        // act
        var resolution = resolver.Resolve(constructorRequest);

        // assert
        resolution.Should().NotBeNull();
        resolution.Resolved.Should().BeTrue();
    }
}

file class Foo
{
    public string Value { get; set; }
}

file class Bar
{
    public string Value { get; set; }
}

file class Baz
{
    public Baz(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

}