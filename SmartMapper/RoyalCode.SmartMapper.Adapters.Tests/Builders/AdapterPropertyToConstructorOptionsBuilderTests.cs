using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Builders;

public class AdapterPropertyToConstructorOptionsBuilderTests
{
    [Fact]
    public void Parameters_Must_CallTheConfigureAction()
    {
        var propertyOptions = new PropertyOptions(typeof(Foo).GetProperty(nameof(Foo.Value))!);
        var constructorOptions = new ConstructorOptions(typeof(Bar));
        var propertyToConstructorOptions = new ToConstructorOptions(propertyOptions, constructorOptions);
        var builder = new AdapterPropertyToConstructorOptionsBuilder<string>(propertyToConstructorOptions);
        
        bool called = false;
        builder.Parameters(b => called = b is not null);

        called.Should().BeTrue();
    }

#pragma warning disable CS8618

    private class Foo
    {
        public string Value { get; set; }
    }
    
    private class Bar { }
}