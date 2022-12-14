
using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Resolvers;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Extensions;

public class ElegibleConstructorsTests
{
    [Fact]
    public void GetElegibleConstructors_Must_FindDefaultConstructor()
    {
        // arrange
        var constructorOptions = new ConstructorOptions(typeof(Bar));

        // act
        var constructors = constructorOptions.CreateEligibleConstructors();

        // assert
        constructors.Should().HaveCount(1);
    }

    [Fact]
    public void GetElegibleConstructors_Must_FindMultiplesConstructors()
    {
        // arrange
        var constructorOptions = new ConstructorOptions(typeof(Baz));
        
        // act
        var constructors = constructorOptions.CreateEligibleConstructors();

        // assert
        constructors.Should().HaveCount(2);
    }

    [Fact]
    public void GetElegibleConstructors_Must_NotIncludeNotElegibleConstructors()
    {
        // arrange
        var constructorOptions = new ConstructorOptions(typeof(Qux));
        constructorOptions.ParameterTypes = new[] { typeof(string) };
        
        // act
        var constructors = constructorOptions.CreateEligibleConstructors();

        // assert
        constructors.Should().HaveCount(1);
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
    public string? Value { get; }

    public Baz(string value)
    {
        Value = value;
    }

    public Baz()
    {

    }
}
file class Qux
{
    public Foo? Foo { get; }

    public string? Value { get; }

    // valid
    public Qux(string value)
    {
        Value = value;
    }

    // invalid when set parameter types, valid otherwise
    public Qux()
    {

    }

    // invalid
    public Qux(Foo value)
    {
        Foo = value;
    }

    // invalid
    public Qux(Foo foo, string value)
    {
        Foo = foo;
        Value = value;
    }

    // invalid
    public Qux(string value, Foo? foo = null)
    {
        Foo = foo;
        Value = value;
    }
}