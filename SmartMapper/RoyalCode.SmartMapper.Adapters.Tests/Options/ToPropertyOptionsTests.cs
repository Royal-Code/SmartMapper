using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Core;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Options;

public class ToPropertyOptionsTests
{
    [Fact]
    public void ThenToProperty_Must_ReturnThenToProperty_And_SetThenTo()
    {
        // arrange
        var property = typeof(Bar).GetProperty(nameof(Bar.Baz))!;
        var toProperty = new ToPropertyOptions(property.DeclaringType!, property);
        
        var propertyOptions = new PropertyOptions(typeof(Foo).GetProperty(nameof(Foo.Value))!);
        propertyOptions.MappedToProperty(toProperty);
        
        var bazProperty = typeof(Baz).GetProperty(nameof(Baz.Value))!;

        // act
        var thenToProperty = toProperty.ThenToProperty(bazProperty);
        
        // assert
        thenToProperty.Should().NotBeNull();
        toProperty.ThenTo.Should().NotBeNull().And.BeSameAs(thenToProperty);
    }

    [Fact]
    public void ThenToProperty_Must_ConfigurePropertyOptions()
    {
        // arrange
        var property = typeof(Bar).GetProperty(nameof(Bar.Baz))!;
        var toProperty = new ToPropertyOptions(property.DeclaringType!, property);
        
        var propertyOptions = new PropertyOptions(typeof(Foo).GetProperty(nameof(Foo.Value))!);
        propertyOptions.MappedToProperty(toProperty);
        
        var bazProperty = typeof(Baz).GetProperty(nameof(Baz.Value))!;

        // act
        var thenToProperty = toProperty.ThenToProperty(bazProperty);
        
        // assert
        propertyOptions.ResolutionStatus.Should().Be(ResolutionStatus.MappedToProperty);
        propertyOptions.ResolutionOptions.Should().BeSameAs(thenToProperty);
        thenToProperty.ResolvedProperty.Should().BeSameAs(propertyOptions);
    }
    
    [Fact]
    public void ThenToMethod_Must_ReturnThenToMethod_And_SetThenTo()
    {
        // arrange
        var property = typeof(Bar).GetProperty(nameof(Bar.Baz))!;
        var toProperty = new ToPropertyOptions(property.DeclaringType!, property);
        
        var propertyOptions = new PropertyOptions(typeof(Foo).GetProperty(nameof(Foo.Value))!);
        propertyOptions.MappedToProperty(toProperty);
        
        var methodOptions = new MethodOptions(typeof(Baz));

        // act
        var thenToMethod = toProperty.ThenToMethod(methodOptions);
        
        // assert
        thenToMethod.Should().NotBeNull();
        toProperty.ThenTo.Should().NotBeNull().And.BeSameAs(thenToMethod);
    }

    [Fact]
    public void ThenToMethod_Must_ConfigurePropertyOptions()
    {
        // arrange
        var property = typeof(Bar).GetProperty(nameof(Bar.Baz))!;
        var toProperty = new ToPropertyOptions(property.DeclaringType!, property);
        
        var propertyOptions = new PropertyOptions(typeof(Foo).GetProperty(nameof(Foo.Value))!);
        propertyOptions.MappedToProperty(toProperty);
        
        var methodOptions = new MethodOptions(typeof(Baz));

        // act
        var thenToMethod = toProperty.ThenToMethod(methodOptions);
        
        // assert
        propertyOptions.ResolutionStatus.Should().Be(ResolutionStatus.MappedToMethod);
        propertyOptions.ResolutionOptions.Should().BeSameAs(thenToMethod);
        thenToMethod.ResolvedProperty.Should().BeSameAs(propertyOptions);
    }
    
#pragma warning disable CS8618
    
    private class Foo
    {
        public string Value { get; set; }
    }
    
    private class Bar
    {
        public string Value { get; set; }
        
        public Baz Baz { get; set; }
    }
    
    private class Baz
    {
        public string Value { get; set; }
        
        public void DoSomething(string value) { }
    }
}