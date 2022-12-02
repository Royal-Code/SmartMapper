
using RoyalCode.SmartMapper.Infrastructure.Discovery;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Discoveries;

public class ConstructorParameterDiscoveryTests
{

    [Fact]
    public void Discover_Must_CreateSuccessMatch_ForContructorWithOneParameterAndSamePropertyName()
    {
        // arrange
        var discoveryContext = new ConstructorParameterDiscoveryContext(
            new ConstructorParameterDiscoveryConfiguration(),
            new[] { new PropertyDiscoveryResult("Name", typeof(string)) },
            new[] { new TargetParameter(typeof(string), "Name") });

        var discovery = new ConstructorParameterDiscovery();

        // act
        var match = discovery.Discover(typeof(Foo), typeof(Bar));

        // assert
        match.Should().NotBeNull();
        match.Resolved.Should().BeTrue();
    }
}
}


file class Foo
{
    public string Value { get; set; }
}

file class Bar
{
    public Baz(string value)
    {
        Value = value;
    }

    public string Value { get; }

}