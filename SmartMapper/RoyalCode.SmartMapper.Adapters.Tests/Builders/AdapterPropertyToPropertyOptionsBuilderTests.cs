using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Builders;

public class AdapterPropertyToPropertyOptionsBuilderTests
{
    [Fact]
    public void CastValue_Must_ConfigureAssignmentStrategy_And_ReturnTheBuilder()
    {
        // Arrange
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.GetPropertyOptions(typeof(Foo).GetProperty("Quux")!);
        var propertyToPropertyOptions = new PropertyToPropertyOptions(typeof(Bar), typeof(Bar).GetProperty("Baz")!);
        propertyOptions.MappedToProperty(propertyToPropertyOptions);

        var builder = new AdapterPropertyToPropertyOptionsBuilder<Foo, Bar, Quux, Baz>(
            adapterOptions, propertyOptions, propertyToPropertyOptions);
        
        // Act
        var returned = builder.CastValue();
        
        // Assert
        returned.Should().NotBeNull();
        propertyOptions.AssignmentStrategy.Should().NotBeNull();
        propertyOptions.AssignmentStrategy!.Strategy.Should().Be(ValueAssignmentStrategy.Cast);
    }

    [Fact]
    public void UseConverter_Must_ConfigureAssignmentStrategy_And_ReturnTheBuilder_And_SetConverterOptionsAnnotation()
    {
            // Arrange
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.GetPropertyOptions(typeof(Foo).GetProperty("Quux")!);
        var propertyToPropertyOptions = new PropertyToPropertyOptions(typeof(Bar), typeof(Bar).GetProperty("Baz")!);
        propertyOptions.MappedToProperty(propertyToPropertyOptions);

        var builder = new AdapterPropertyToPropertyOptionsBuilder<Foo, Bar, Quux, Baz>(
            adapterOptions, propertyOptions, propertyToPropertyOptions);
        
        // Act
        var returned = builder.UseConverter(q => new Baz(){ Value = q.Value});
        
        // Assert
        returned.Should().NotBeNull();
        
        propertyOptions.AssignmentStrategy.Should().NotBeNull();
        propertyOptions.AssignmentStrategy!.Strategy.Should().Be(ValueAssignmentStrategy.Convert);
        
        var convertOptions = propertyOptions.AssignmentStrategy!.FindAnnotation<ConvertOptions>();
        convertOptions.Should().NotBeNull();
        convertOptions!.Converter.Should().NotBeNull();
    }

    [Fact]
    public void Adapt_Must_ConfigureAssignmentStrategy_And_ReturnTheBuilder()
    {
        // Arrange
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.GetPropertyOptions(typeof(Foo).GetProperty("Quux")!);
        var propertyToPropertyOptions = new PropertyToPropertyOptions(typeof(Bar), typeof(Bar).GetProperty("Baz")!);
        propertyOptions.MappedToProperty(propertyToPropertyOptions);

        var builder = new AdapterPropertyToPropertyOptionsBuilder<Foo, Bar, Quux, Baz>(
            adapterOptions, propertyOptions, propertyToPropertyOptions);
        
        // Act
        var returned = builder.Adapt();
        
        // Assert
        returned.Should().NotBeNull();
        propertyOptions.AssignmentStrategy.Should().NotBeNull();
        propertyOptions.AssignmentStrategy!.Strategy.Should().Be(ValueAssignmentStrategy.Adapt);
    }
    
    [Fact]
    public void Select_Must_ConfigureAssignmentStrategy_And_ReturnTheBuilder()
    {
        // Arrange
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.GetPropertyOptions(typeof(Foo).GetProperty("Quux")!);
        var propertyToPropertyOptions = new PropertyToPropertyOptions(typeof(Bar), typeof(Bar).GetProperty("Baz")!);
        propertyOptions.MappedToProperty(propertyToPropertyOptions);

        var builder = new AdapterPropertyToPropertyOptionsBuilder<Foo, Bar, Quux, Baz>(
            adapterOptions, propertyOptions, propertyToPropertyOptions);
        
        // Act
        var returned = builder.Select();
        
        // Assert
        returned.Should().NotBeNull();
        propertyOptions.AssignmentStrategy.Should().NotBeNull();
        propertyOptions.AssignmentStrategy!.Strategy.Should().Be(ValueAssignmentStrategy.Select);
    }
    
    [Fact]
    public void WithService_Must_ConfigureAssignmentStrategy_And_ReturnTheBuilder_And_SetProcessorOptionsAnnotation()
    {
        // Arrange
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.GetPropertyOptions(typeof(Foo).GetProperty("Quux")!);
        var propertyToPropertyOptions = new PropertyToPropertyOptions(typeof(Bar), typeof(Bar).GetProperty("Baz")!);
        propertyOptions.MappedToProperty(propertyToPropertyOptions);

        var builder = new AdapterPropertyToPropertyOptionsBuilder<Foo, Bar, Quux, Baz>(
            adapterOptions, propertyOptions, propertyToPropertyOptions);
        
        // Act
        var returned = builder.WithService<Processor>((p, q) => p.Process(q));
        
        // Assert
        returned.Should().NotBeNull();
        
        propertyOptions.AssignmentStrategy.Should().NotBeNull();
        propertyOptions.AssignmentStrategy!.Strategy.Should().Be(ValueAssignmentStrategy.Processor);
        
        var processorOptions = propertyOptions.AssignmentStrategy!.FindAnnotation<ProcessorOptions>();
        processorOptions.Should().NotBeNull();
        processorOptions!.Processor.Should().NotBeNull();
    }
    
    public class Foo
    {
        public string Value { get; set; }

        public Quux Quux { get; set; }
    }
    
    public class Bar
    {
        public string Value { get; set; }

        public Baz Baz { get; set; }
    }
    
    public class Quux
    {
        public string Value { get; set; }
    }

    public class Baz
    {
        public string Value { get; set; }
    }
    
    public class Processor
    {
        public Baz Process(Quux quux)
        {
            return new Baz(){ Value = quux.Value };
        }
    }
}