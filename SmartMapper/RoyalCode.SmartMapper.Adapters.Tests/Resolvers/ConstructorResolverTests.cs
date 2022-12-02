using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using System.Reflection;
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
        var contructorContext = new ConstructorContext(adapterContext, typeof(Bar).GetConstructor(Type.EmptyTypes)!);

        var resolver = new ConstructorResolver();

        // act
        var resolution = resolver.Resolve(contructorContext);

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
        var contructorContext = new ConstructorContext(adapterContext, typeof(Baz).GetConstructor(new[] { typeof(string) })!);

        var resolver = new ConstructorResolver();

        // act
        var resolution = resolver.Resolve(contructorContext);

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