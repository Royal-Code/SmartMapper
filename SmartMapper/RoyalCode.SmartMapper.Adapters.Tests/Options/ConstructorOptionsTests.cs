using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Options;

public class ConstructorOptionsTests
{
    [Fact]
    public void GetParameterOptions_Must_ReturnSameOptionsPerSourceProperty()
    {
        // Arrange
        var constructorOptions = new ConstructorOptions(typeof(Bar));

        // act
        var some1 = constructorOptions.GetParameterOptions(typeof(Foo).GetProperty(nameof(Foo.SomeValue))!);
        var some2 = constructorOptions.GetParameterOptions(typeof(Foo).GetProperty(nameof(Foo.SomeValue))!);
        var other = constructorOptions.GetParameterOptions(typeof(Foo).GetProperty(nameof(Foo.OtherValue))!);
        
        // assert
        some2.Should().NotBeNull();
        some1.Should().NotBeNull()
            .And.BeSameAs(some2)
            .And.NotBeSameAs(other);
    }

    [Fact]
    public void TryGetParameterOptions_Must_ReturnFalseAndNull_When_PropertyNotConfigured()
    {
        // Arrange
        var constructorOptions = new ConstructorOptions(typeof(Bar));
        
        // act
        var found = constructorOptions.TryGetParameterOptions(
            typeof(Foo).GetProperty(nameof(Foo.SomeValue))!,
            out var options);
        
        // assert
        found.Should().BeFalse();
        options.Should().BeNull();
    }

    [Fact]
    public void TryGetParameterOptions_Must_ReturnTrueAndTheOptions_When_PropertyConfigured()
    {
        // Arrange
        var constructorOptions = new ConstructorOptions(typeof(Bar));
        var property = typeof(Foo).GetProperty(nameof(Foo.SomeValue))!;
        constructorOptions.GetParameterOptions(property);
            
        // act
        var found = constructorOptions.TryGetParameterOptions(
            typeof(Foo).GetProperty(nameof(Foo.SomeValue))!,
            out var options);
        
        // assert
        found.Should().BeTrue();
        options.Should().NotBeNull();
    }
    
    private class Foo
    {
        public int SomeValue { get; set; }

        public int OtherValue { get; set; }
    }
    
    private class Bar { }
}