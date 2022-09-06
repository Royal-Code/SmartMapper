using System;
using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Options;

public class PropertyOptionsTests
{
    [Fact]
    public void MappedToMethodParameter_Must_SetResolvedProperty()
    {
        var methodOptions = new MethodOptions(typeof(Bar));
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var propertyToParameterOptions = new ToMethodParameterOptions(methodOptions, propertyInfo);
        var options = new PropertyOptions(propertyInfo);
        
        options.MappedToMethodParameter(propertyToParameterOptions);

        propertyToParameterOptions.ResolvedProperty.Should().NotBeNull().And.BeSameAs(options);
    }

    [Fact]
    public void MappedToMethodParameter_Must_SetResolutionOptions()
    {
        var methodOptions = new MethodOptions(typeof(Bar));
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var propertyToParameterOptions = new ToMethodParameterOptions(methodOptions, propertyInfo);
        var options = new PropertyOptions(propertyInfo);
        
        options.MappedToMethodParameter(propertyToParameterOptions);

        options.ResolutionOptions.Should().NotBeNull().And.BeSameAs(propertyToParameterOptions);
    }
    
    [Fact]
    public void MappedToMethodParameter_Must_Throw_When_ResolutionStatusIsSetPreviously()
    {
        var methodOptions = new MethodOptions(typeof(Bar));
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var propertyToParameterOptions = new ToMethodParameterOptions(methodOptions, propertyInfo);
        var options = new PropertyOptions(propertyInfo);
        
        options.IgnoreMapping();
        
        var action = () => options.MappedToMethodParameter(propertyToParameterOptions);
        
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void MappedToConstructorParameter_Must_SetResolvedProperty()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var toConstructorParameter = new ToConstructorParameterOptions(typeof(Bar), propertyInfo);
        var options = new PropertyOptions(propertyInfo);

        options.MappedToConstructorParameter(toConstructorParameter);

        toConstructorParameter.ResolvedProperty.Should().NotBeNull().And.BeSameAs(options);
    }

    [Fact]
    public void MappedToConstructorParameter_Must_SetResolutionOptions()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var toConstructorParameter = new ToConstructorParameterOptions(typeof(Bar), propertyInfo);
        var options = new PropertyOptions(propertyInfo);

        options.MappedToConstructorParameter(toConstructorParameter);

        options.ResolutionOptions.Should().NotBeNull().And.BeSameAs(toConstructorParameter);
    }

    [Fact]
    public void MappedToConstructorParameter_Must_Throw_When_ResolutionStatusIsSetPreviously()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var toConstructorParameter = new ToConstructorParameterOptions(typeof(Bar), propertyInfo);
        var options = new PropertyOptions(propertyInfo);
        
        options.IgnoreMapping();

        var action = () => options.MappedToConstructorParameter(toConstructorParameter);

        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void MappedToProperty_Must_SetResolvedProperty()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);

        var propertyToProperty = new ToPropertyOptions(typeof(Bar), typeof(Bar).GetProperty("Value")!);

        options.MappedToProperty(propertyToProperty);
        
        propertyToProperty.ResolvedProperty.Should().NotBeNull().And.BeSameAs(options);
    }

    [Fact]
    public void MappedToProperty_Must_SetResolutionOptions()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);

        var propertyToProperty = new ToPropertyOptions(typeof(Bar), typeof(Bar).GetProperty("Value")!);

        options.MappedToProperty(propertyToProperty);

        options.ResolutionOptions.Should().NotBeNull().And.BeSameAs(propertyToProperty);
    }

    [Fact]
    public void MappedToProperty_Must_Throw_When_ResolutionStatusIsSetPreviously()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);
        options.IgnoreMapping();
        
        var propertyToProperty = new ToPropertyOptions(typeof(Bar), typeof(Bar).GetProperty("Value")!);

        var action = () => options.MappedToProperty(propertyToProperty);
        
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void IgnoreMapping_Must_Throw_When_ResolutionStatusIsSetPreviously()
    {
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = new PropertyOptions(propertyInfo);
        var propertyToProperty = new ToPropertyOptions(typeof(Bar), typeof(Bar).GetProperty("Value")!);
        
        options.MappedToProperty(propertyToProperty);
        
        var action = () => options.IgnoreMapping();
        
        action.Should().Throw<InvalidOperationException>();
    }
    
    [Fact]
    public void MappedToMethod_Must_SetResolvedProperty()
    {
        // arrange
        var propertyOptions = new PropertyOptions(typeof(Foo).GetProperty(nameof(Foo.Value))!);
        var methodOptions = new MethodOptions(typeof(Bar));
        var toMethod = new ToMethodOptions(propertyOptions, methodOptions);
        
        // act
        propertyOptions.MappedToMethod(toMethod);

        // assert
        toMethod.ResolvedProperty.Should().NotBeNull().And.BeSameAs(propertyOptions);
    }

    [Fact]
    public void MappedToMethod_Must_SetResolutionOptions()
    {
        // arrange
        var propertyOptions = new PropertyOptions(typeof(Foo).GetProperty(nameof(Foo.Value))!);
        var methodOptions = new MethodOptions(typeof(Bar));
        var toMethod = new ToMethodOptions(propertyOptions, methodOptions);
        
        // act
        propertyOptions.MappedToMethod(toMethod);

        // assert
        propertyOptions.ResolutionOptions.Should().NotBeNull().And.BeSameAs(toMethod);
    }

    [Fact]
    public void MappedToMethod_Must_Throw_When_ResolutionStatusIsSetPreviously()
    {
        // arrange
        var propertyOptions = new PropertyOptions(typeof(Foo).GetProperty(nameof(Foo.Value))!);
        var methodOptions = new MethodOptions(typeof(Bar));
        var toMethod = new ToMethodOptions(propertyOptions, methodOptions);
        propertyOptions.IgnoreMapping();
        
        var action = () => propertyOptions.MappedToMethod(toMethod);
        
        // assert
        action.Should().Throw<InvalidOperationException>();
    }
    
    [Fact]
    public void MappedToConstructor_Must_SetResolvedProperty()
    {
        // arrange
        var property = typeof(Foo).GetProperty(nameof(Foo.Value))!;
        var propertyOptions = new PropertyOptions(property);
        var targetOptions = new TargetOptions(typeof(Bar));
        var constructorOptions = targetOptions.GetConstructorOptions();
        var toConstructor = new ToConstructorOptions(propertyOptions, constructorOptions);
        
        // act
        propertyOptions.MappedToConstructor(toConstructor);

        // assert
        toConstructor.ResolvedProperty.Should().NotBeNull().And.BeSameAs(propertyOptions);
    }

    [Fact]
    public void MappedToConstructor_Must_SetResolutionOptions()
    {
        // arrange
        var property = typeof(Foo).GetProperty(nameof(Foo.Value))!;
        var propertyOptions = new PropertyOptions(property);
        var targetOptions = new TargetOptions(typeof(Bar));
        var constructorOptions = targetOptions.GetConstructorOptions();
        var toConstructor = new ToConstructorOptions(propertyOptions, constructorOptions);
        
        // act
        propertyOptions.MappedToConstructor(toConstructor);
        
        // assert
        propertyOptions.ResolutionOptions.Should().NotBeNull().And.BeSameAs(toConstructor);
    }

    [Fact]
    public void MappedToConstructor_Must_Throw_When_ResolutionStatusIsSetPreviously()
    {
        // arrange
        var property = typeof(Foo).GetProperty(nameof(Foo.Value))!;
        var propertyOptions = new PropertyOptions(property);
        var targetOptions = new TargetOptions(typeof(Bar));
        var constructorOptions = targetOptions.GetConstructorOptions();
        var toConstructor = new ToConstructorOptions(propertyOptions, constructorOptions);
        
        propertyOptions.IgnoreMapping();
        
        // act
        Action act = () => propertyOptions.MappedToConstructor(toConstructor);
        
        // assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void ThenMapTo_Must_Throw_IfNotMappedBefore()
    {
        // arrange
        var property = typeof(Foo).GetProperty(nameof(Foo.Value))!;
        var propertyOptions = new PropertyOptions(property);

        var sourceToProperty = new ToPropertyOptions(typeof(Bar), typeof(Bar).GetProperty(nameof(Bar.Baz))!);
        var targetToProperty = new ToPropertyOptions(typeof(Baz), typeof(Baz).GetProperty(nameof(Baz.Quux))!);
        var thenToOptions = new ThenToPropertyOptions(sourceToProperty, targetToProperty);
        
        // act
        Action act = () => propertyOptions.ThenMappedTo(thenToOptions);

        // assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void ThenMapTo_Must_Throw_IfNotMappedToPropertyBefore()
    {
        // arrange
        var property = typeof(Foo).GetProperty(nameof(Foo.Value))!;
        var propertyOptions = new PropertyOptions(property);
        var toConstructorParameter = new ToConstructorParameterOptions(typeof(Bar), property);
        propertyOptions.MappedToConstructorParameter(toConstructorParameter);
        
        var sourceToProperty = new ToPropertyOptions(typeof(Bar), typeof(Bar).GetProperty(nameof(Bar.Baz))!);
        var targetToProperty = new ToPropertyOptions(typeof(Baz), typeof(Baz).GetProperty(nameof(Baz.Quux))!);
        var thenToOptions = new ThenToPropertyOptions(sourceToProperty, targetToProperty);
        
        // act
        Action act = () => propertyOptions.ThenMappedTo(thenToOptions);

        // assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void ThenMapTo_Must_UpdateResolutionOptions_And_SetResolvedProperty()
    {
        // arrange
        var property = typeof(Foo).GetProperty(nameof(Foo.Value))!;
        var propertyOptions = new PropertyOptions(property);
        
        var sourceToProperty = new ToPropertyOptions(typeof(Bar), typeof(Bar).GetProperty(nameof(Bar.Baz))!);
        var targetToProperty = new ToPropertyOptions(typeof(Baz), typeof(Baz).GetProperty(nameof(Baz.Quux))!);
        var thenToOptions = new ThenToPropertyOptions(sourceToProperty, targetToProperty);
        
        propertyOptions.MappedToProperty(sourceToProperty);
        
        // act
        propertyOptions.ThenMappedTo(thenToOptions);
        
        // assert
        propertyOptions.ResolutionOptions.Should().NotBeNull().And.BeSameAs(thenToOptions);
        thenToOptions.ResolvedProperty.Should().NotBeNull().And.BeSameAs(propertyOptions);
    }

    [Fact]
    public void ThenMapTo_Must_SetPreviousThenTo()
    {
        // arrange
        var property = typeof(Foo).GetProperty(nameof(Foo.Value))!;
        var propertyOptions = new PropertyOptions(property);
        
        var sourceToProperty = new ToPropertyOptions(typeof(Bar), typeof(Bar).GetProperty(nameof(Bar.Baz))!);
        var targetToProperty = new ToPropertyOptions(typeof(Baz), typeof(Baz).GetProperty(nameof(Baz.Quux))!);
        var thenToOptions = new ThenToPropertyOptions(sourceToProperty, targetToProperty);
        
        var nextTargetToProperty = new ToPropertyOptions(typeof(Quux), typeof(Quux).GetProperty(nameof(Quux.Value))!);
        var nextThenToOptions = new ThenToPropertyOptions(targetToProperty, nextTargetToProperty);
        
        propertyOptions.MappedToProperty(sourceToProperty);
        propertyOptions.ThenMappedTo(thenToOptions);
        
        // act
        propertyOptions.ThenMappedTo(nextThenToOptions);
        
        // assert
        nextThenToOptions.Previous.Should().NotBeNull();
    }
    
#pragma warning disable CS8618

    private class Foo
    {
        public string Value { get; set; }
    }

    private class Bar
    {
        public Baz Baz { get; set; }
    }
    
    private class Baz
    {
        public Quux Quux { get; set; }
        
        public void DoSomething(string input) { }
    }
    
    private class Quux
    {
        public string Value { get; set; }

        public void DoOtherthing(string input) { }
    }
}