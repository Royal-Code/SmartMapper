using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Resolutions;

public class ActivationResolutionTests
{
    [Fact]
    public void GetElegibleConstructors_Must_FindDefaultConstructor()
    {
        // arrange
        var configs = new ResolutionConfiguration();
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var adapterContext = new AdapterResolutionContext(adapterOptions, configs);

        var resolver = new ActivationResolver();

        // act
        var constructors = resolver.GetElegibleConstructors(adapterContext.GetConstructorOptions());

        // assert
        constructors.Should().HaveCount(1);
    }
    
    private class Foo
    {
        public string Value { get; set; }
    }
    
    private class Bar
    {
        public string Value { get; set; }
    }
}