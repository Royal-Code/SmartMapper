using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Builders;

public class AdapterPropertyToConstructorOptionsBuilderTests
{
    [Fact]
    public void Parameters_Must_CallTheConfigureAction()
    {
        var constructorOptions = new ConstructorOptions(typeof(Bar));
        var propertyToConstructorOptions = new PropertyToConstructorOptions(typeof(string), constructorOptions);
        var builder = new AdapterPropertyToConstructorOptionsBuilder<Foo, string>(propertyToConstructorOptions);
        
        bool called = false;
        builder.Parameters(b => called = b is not null);

        called.Should().BeTrue();
    }

    private class Foo
    {
        public string Value { get; set; }
    }
    
    private class Bar { }
}