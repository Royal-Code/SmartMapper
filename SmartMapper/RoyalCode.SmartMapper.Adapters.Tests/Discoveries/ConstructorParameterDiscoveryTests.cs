
using RoyalCode.SmartMapper.Infrastructure.Discovery;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Discoveries;

public class ConstructorParameterDiscoveryTests
{

    [Fact]
    public void Discover_Must_CreateSuccessMatch_ForContructorWithOneParameterAndSamePropertyName()
    {
        
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