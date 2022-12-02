using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Core;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Resolvers;

public class AdapterResolverTests
{
    // TODO: Add tests for the adapter resolver
    [Fact]
    public void Resolve_Must_CreateSuccessResolution_ForSimpleTypes_WithOnePropertyWithSameName()
    {
        // arrange
        var configs = ConfigurationBuilder.CreateDefault().Build();
        var adapterContext = new AdapterRequest(new MapKey(typeof(Foo), typeof(Bar)), configs);

        var resolver = new AdapterResolver();

        // act
        var resolution = resolver.Resolve(adapterContext);

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