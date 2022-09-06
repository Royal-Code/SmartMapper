using System;
using FluentAssertions;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Core;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Builders;

public class AdapterPropertyToConstructorParametersOptionsBuilderTests
{
    [Fact]
    public void Ignore_Must_ConfigureThePropertyToBeIgnored()
    {
        // arrange
        var sourceOptions = new SourceOptions(typeof(Bar));
        var constructorOptions = new ConstructorOptions(typeof(Qux));
        
        var builder = new AdapterPropertyToConstructorParametersOptionsBuilder<Bar>(sourceOptions, constructorOptions);
        
        // act
        builder.Ignore(b => b.OtherValue);
        
        // assert
        var found = sourceOptions.TryGetPropertyOptions(nameof(Bar.OtherValue), out var options);
        found.Should().BeTrue();
        options.Should().NotBeNull();
        options!.ResolutionStatus.Should().Be(ResolutionStatus.Ignored);
    }

    [Fact]
    public void Parameter_Must_Throw_When_SelectorIsNotAProperty()
    {
        // arrange
        var sourceOptions = new SourceOptions(typeof(Bar));
        var constructorOptions = new ConstructorOptions(typeof(Qux));
        
        var builder = new AdapterPropertyToConstructorParametersOptionsBuilder<Bar>(sourceOptions, constructorOptions);

        // act
        Action act = () => builder.Parameter(b => string.Empty);
        
        // assert
        act.Should().Throw<InvalidPropertySelectorException>();
    }

    [Fact]
    public void Parameter_Must_ConfigureTheProperty_With_MappedToConstructorParameter_And_ReturnTheBuilder()
    {
        // arrange
        var sourceOptions = new SourceOptions(typeof(Bar));
        var constructorOptions = new ConstructorOptions(typeof(Qux));
        
        var builder = new AdapterPropertyToConstructorParametersOptionsBuilder<Bar>(sourceOptions, constructorOptions);
        
        // act
        var returned = builder.Parameter(b => b.Value);
        
        // assert
        returned.Should().NotBeNull();

        var found = sourceOptions.TryGetPropertyOptions(nameof(Bar.Value), out var propertyOptions);
        found.Should().BeTrue();
        propertyOptions.Should().NotBeNull();
        propertyOptions!.ResolutionStatus.Should().Be(ResolutionStatus.MappedToConstructorParameter);

        propertyOptions.ResolutionOptions.Should().BeOfType<ToConstructorParameterOptions>();
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("NotAParameter", false)]
    [InlineData("someValue", true)]
    public void Parameter_Must_Throw_When_ParameterNameIsInvalid(string name, bool isValid)
    {
        // arrange
        var sourceOptions = new SourceOptions(typeof(Bar));
        var constructorOptions = new ConstructorOptions(typeof(Qux));
        
        var builder = new AdapterPropertyToConstructorParametersOptionsBuilder<Bar>(sourceOptions, constructorOptions);
        
        // act
        Action act = () => builder.Parameter(b => b.Value, name);
        
        // assert
        if (isValid)
            act.Should().NotThrow();
        else
            act.Should().Throw<InvalidParameterNameException>();
    }
    
#pragma warning disable CS8618

    private class Foo
    {
        public Bar Value { get; set; }
    }

    private class Bar
    {
        public string Value { get; set; }
        
        public string OtherValue { get; set; }
    }
    
    private class Qux
    {
        public string SomeValue { get; }

        public Qux(string someValue)
        {
            SomeValue = someValue;
        }
    }
}