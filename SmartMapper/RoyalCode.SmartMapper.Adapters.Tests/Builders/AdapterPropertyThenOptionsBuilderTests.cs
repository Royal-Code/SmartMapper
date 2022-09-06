using System;
using FluentAssertions;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Builders;

public class AdapterPropertyThenOptionsBuilderTests
{
    [Fact]
    public void To_Must_Throw_When_SelectorIsNotAProperty()
    {
        // Arrange
        var toOptions = new ToPropertyOptions(typeof(Foo), typeof(Foo).GetProperty(nameof(Foo.Bar))!);
        var propertyOptions = new PropertyOptions(typeof(Quux).GetProperty(nameof(Quux.Value))!);
        propertyOptions.MappedToProperty(toOptions);
        
        var builder = new AdapterPropertyThenOptionsBuilder<string, Bar>(toOptions);
        
        // Act
        Action action = () => builder.To(q => q.DoSomething());
        
        // Assert
        action.Should().Throw<InvalidPropertySelectorException>();
    }
    
    [Fact]
    public void To_Must_Set_ThenToOptions_And_ReturnTheBuilder()
    {
        // Arrange
        var toOptions = new ToPropertyOptions(typeof(Foo), typeof(Foo).GetProperty(nameof(Foo.Bar))!);
        var propertyOptions = new PropertyOptions(typeof(Quux).GetProperty(nameof(Quux.Value))!);
        propertyOptions.MappedToProperty(toOptions);
        
        var builder = new AdapterPropertyThenOptionsBuilder<string, Bar>(toOptions);
        
        // Act
        var returned = builder.To(q => q.Baz);
        
        // Assert
        returned.Should().NotBeNull();
        toOptions.ThenTo.Should().NotBeNull();
    }

    [Fact]
    public void To_Must_Throw_When_InvalidPropertyName()
    {
        // Arrange
        var toOptions = new ToPropertyOptions(typeof(Foo), typeof(Foo).GetProperty(nameof(Foo.Bar))!);
        var propertyOptions = new PropertyOptions(typeof(Quux).GetProperty(nameof(Quux.Value))!);
        propertyOptions.MappedToProperty(toOptions);
        
        var builder = new AdapterPropertyThenOptionsBuilder<string, Bar>(toOptions);
        
        // Act
        Action action = () => builder.To<string>("Not a property");
        
        // Assert
        action.Should().Throw<InvalidPropertyNameException>();
    }

    [Fact]
    public void To_Must_Throw_When_InvalidPropertyType()
    {
        // Arrange
        var toOptions = new ToPropertyOptions(typeof(Foo), typeof(Foo).GetProperty(nameof(Foo.Bar))!);
        var propertyOptions = new PropertyOptions(typeof(Quux).GetProperty(nameof(Quux.Value))!);
        propertyOptions.MappedToProperty(toOptions);
        
        var builder = new AdapterPropertyThenOptionsBuilder<string, Bar>(toOptions);
        
        // Act
        Action action = () => builder.To<string>(nameof(Bar.Baz));
        
        // Assert
        action.Should().Throw<InvalidPropertyTypeException>();
    }

    [Fact]
    public void To_Must_Set_ThenToOptions_And_ReturnTheBuilder_When_NameAndTypeIsValid()
    {
        // Arrange
        var toOptions = new ToPropertyOptions(typeof(Foo), typeof(Foo).GetProperty(nameof(Foo.Bar))!);
        var propertyOptions = new PropertyOptions(typeof(Quux).GetProperty(nameof(Quux.Value))!);
        propertyOptions.MappedToProperty(toOptions);
        
        var builder = new AdapterPropertyThenOptionsBuilder<string, Bar>(toOptions);
        
        // Act
        var returned = builder.To<Baz>(nameof(Bar.Baz));
        
        // Assert
        returned.Should().NotBeNull();
        toOptions.ThenTo.Should().NotBeNull();
    }

    [Fact]
    public void ToMethod_Must_Set_ThenToOptions_And_ReturnTheBuilder()
    {
        // Arrange
        var toOptions = new ToPropertyOptions(typeof(Foo), typeof(Foo).GetProperty(nameof(Foo.Bar))!);
        var propertyOptions = new PropertyOptions(typeof(Quux).GetProperty(nameof(Quux.Value))!);
        propertyOptions.MappedToProperty(toOptions);
        
        var builder = new AdapterPropertyThenOptionsBuilder<string, Bar>(toOptions);
        
        // Act
        var returned = builder.ToMethod();
        
        // Assert
        returned.Should().NotBeNull();
        toOptions.ThenTo.Should().NotBeNull();
    }
    
    [Fact]
    public void ToMethod_With_Delegate_Must_Throw_When_InvalidDelegate()
    {
        // Arrange
        var toOptions = new ToPropertyOptions(typeof(Foo), typeof(Foo).GetProperty(nameof(Foo.Bar))!);
        var propertyOptions = new PropertyOptions(typeof(Quux).GetProperty(nameof(Quux.Value))!);
        propertyOptions.MappedToProperty(toOptions);
        
        var builder = new AdapterPropertyThenOptionsBuilder<string, Bar>(toOptions);

        // Act
        var act = () => builder.ToMethod(x => string.IsNullOrEmpty);
        
        act.Should().Throw<InvalidMethodDelegateException>();
    }
    
    [Fact]
    public void ToMethod_With_Delegate_Must_Set_ThenToOptions_And_ReturnTheBuilder()
    {
        // Arrange
        var toOptions = new ToPropertyOptions(typeof(Foo), typeof(Foo).GetProperty(nameof(Foo.Bar))!);
        var propertyOptions = new PropertyOptions(typeof(Quux).GetProperty(nameof(Quux.Value))!);
        propertyOptions.MappedToProperty(toOptions);
        
        var builder = new AdapterPropertyThenOptionsBuilder<string, Bar>(toOptions);
        
        // Act
        var returned = builder.ToMethod(b => b.DoSomething);
        
        // Assert
        returned.Should().NotBeNull();
        toOptions.ThenTo.Should().NotBeNull();
    }
    
    [Fact]
    public void Then_Must_ReturnTheBuilder()
    {
        // Arrange
        var toOptions = new ToPropertyOptions(typeof(Foo), typeof(Foo).GetProperty(nameof(Foo.Bar))!);
        var propertyOptions = new PropertyOptions(typeof(Quux).GetProperty(nameof(Quux.Value))!);
        propertyOptions.MappedToProperty(toOptions);
        var thenToOptions = toOptions.ThenToProperty(typeof(Bar).GetProperty(nameof(Bar.Baz))!);

        var builder = new AdapterPropertyThenOptionsBuilder<string, Bar, Baz>(thenToOptions);
        
        // Act
        var returned = builder.Then();
        
        // Assert
        returned.Should().NotBeNull();
    }

    [Fact]
    public void CastValue_Must_ConfigureAssignmentStrategyOptions()
    {
        // Arrange
        var toOptions = new ToPropertyOptions(typeof(Foo), typeof(Foo).GetProperty(nameof(Foo.Bar))!);
        var propertyOptions = new PropertyOptions(typeof(Quux).GetProperty(nameof(Quux.Value))!);
        propertyOptions.MappedToProperty(toOptions);
        var thenToOptions = toOptions.ThenToProperty(typeof(Bar).GetProperty(nameof(Bar.Baz))!);

        var builder = new AdapterPropertyThenOptionsBuilder<string, Bar, Baz>(thenToOptions);
        
        // act
        builder.CastValue();
        
        // Assert
        propertyOptions.AssignmentStrategy.Should().NotBeNull();
        propertyOptions.AssignmentStrategy!.Strategy.Should().Be(ValueAssignmentStrategy.Cast);
    }

    [Fact]
    public void UseConverter_Must_ConfigureAssignmentStrategyOptions_And_SetConverterOptionsAnnotation()
    {
        // Arrange
        var toOptions = new ToPropertyOptions(typeof(Foo), typeof(Foo).GetProperty(nameof(Foo.Bar))!);
        var propertyOptions = new PropertyOptions(typeof(Quux).GetProperty(nameof(Quux.Value))!);
        propertyOptions.MappedToProperty(toOptions);
        var thenToOptions = toOptions.ThenToProperty(typeof(Bar).GetProperty(nameof(Bar.Baz))!);

        var builder = new AdapterPropertyThenOptionsBuilder<string, Bar, Baz>(thenToOptions);
        
        // Act
        builder.UseConverter(q => new Baz(){ Value = q});
        
        propertyOptions.AssignmentStrategy.Should().NotBeNull();
        propertyOptions.AssignmentStrategy!.Strategy.Should().Be(ValueAssignmentStrategy.Convert);
        
        var convertOptions = propertyOptions.AssignmentStrategy!.FindAnnotation<ConvertOptions>();
        convertOptions.Should().NotBeNull();
        convertOptions!.Converter.Should().NotBeNull();
    }

    [Fact]
    public void Adapt_Must_ConfigureAssignmentStrategy()
    {
        // Arrange
        var toOptions = new ToPropertyOptions(typeof(Foo), typeof(Foo).GetProperty(nameof(Foo.Bar))!);
        var propertyOptions = new PropertyOptions(typeof(Quux).GetProperty(nameof(Quux.Value))!);
        propertyOptions.MappedToProperty(toOptions);
        var thenToOptions = toOptions.ThenToProperty(typeof(Bar).GetProperty(nameof(Bar.Baz))!);

        var builder = new AdapterPropertyThenOptionsBuilder<string, Bar, Baz>(thenToOptions);
        
        // Act
        builder.Adapt();
        
        // Assert
        propertyOptions.AssignmentStrategy.Should().NotBeNull();
        propertyOptions.AssignmentStrategy!.Strategy.Should().Be(ValueAssignmentStrategy.Adapt);
    }

    [Fact]
    public void Select_Must_ConfigureAssignmentStrategy()
    {
        // Arrange
        var toOptions = new ToPropertyOptions(typeof(Foo), typeof(Foo).GetProperty(nameof(Foo.Bar))!);
        var propertyOptions = new PropertyOptions(typeof(Quux).GetProperty(nameof(Quux.Value))!);
        propertyOptions.MappedToProperty(toOptions);
        var thenToOptions = toOptions.ThenToProperty(typeof(Bar).GetProperty(nameof(Bar.Baz))!);

        var builder = new AdapterPropertyThenOptionsBuilder<string, Bar, Baz>(thenToOptions);
        
        // Act
        builder.Select();
        
        // Assert
        propertyOptions.AssignmentStrategy.Should().NotBeNull();
        propertyOptions.AssignmentStrategy!.Strategy.Should().Be(ValueAssignmentStrategy.Select);
    }

    [Fact]
    public void WithService_Must_ConfigureAssignmentStrategy_And_SetProcessorOptionsAnnotation()
    {
        // Arrange
        var toOptions = new ToPropertyOptions(typeof(Foo), typeof(Foo).GetProperty(nameof(Foo.Bar))!);
        var propertyOptions = new PropertyOptions(typeof(Quux).GetProperty(nameof(Quux.Value))!);
        propertyOptions.MappedToProperty(toOptions);
        var thenToOptions = toOptions.ThenToProperty(typeof(Bar).GetProperty(nameof(Bar.Baz))!);

        var builder = new AdapterPropertyThenOptionsBuilder<string, Bar, Baz>(thenToOptions);
        
        // Act
        builder.WithService<Processor>((p, q) => p.Process(q));
        
        // Assert
        propertyOptions.AssignmentStrategy.Should().NotBeNull();
        propertyOptions.AssignmentStrategy!.Strategy.Should().Be(ValueAssignmentStrategy.Processor);
        
        var processorOptions = propertyOptions.AssignmentStrategy!.FindAnnotation<ProcessorOptions>();
        processorOptions.Should().NotBeNull();
        processorOptions!.Processor.Should().NotBeNull();
    }
    
    public class Quux
    {
        public string Value { get; set; }
    }
    
    public class Foo
    {
        public Bar Bar { get; set; }
    }
    
    public class Bar
    {
        public Baz Baz { get; set; }
        
        public string DoSomething() => string.Empty;
    }
    
    public class Baz
    {
        public string Value { get; set; }
    }
    
    public class Processor
    {
        public Baz Process(string value)
        {
            return new Baz(){ Value = value };
        }
    }
}