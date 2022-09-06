using System.Linq;
using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Options;

public class SourceToMethodOptionsTests
{
    [Fact]
    public void GetAllParameterSequence_Must_ReturnEmpty_When_NotConfigurated()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var sourceToMethodOptions = adapterOptions.CreateSourceToMethodOptions();

        sourceToMethodOptions.GetAllParameterSequence().Should().BeEmpty();
    }

    [Fact]
    public void GetAllParameterSequence_Must_ReturnTheParametersInOrder()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var sourceToMethodOptions = adapterOptions.CreateSourceToMethodOptions();
        sourceToMethodOptions.Strategy = SourceToMethodStrategy.SelectedParameters;

        var propertyInfo1 = typeof(Foo).GetProperty("Value")!;
        var options1 = adapterOptions.SourceOptions.GetPropertyOptions(propertyInfo1);
        sourceToMethodOptions.AddPropertyToParameterSequence(options1);

        var propertyInfo2 = typeof(Foo).GetProperty("Description")!;
        var options2 = adapterOptions.SourceOptions.GetPropertyOptions(propertyInfo2);
        sourceToMethodOptions.AddPropertyToParameterSequence(options2);

        var sequence = sourceToMethodOptions.GetAllParameterSequence().ToList();
        sequence.Should().HaveCount(2);
        sequence[0].ResolvedProperty.Should().BeSameAs(options1);
        sequence[1].ResolvedProperty.Should().BeSameAs(options2);
    }
    
#pragma warning disable CS8618

    private class Foo
    {
        public string Value { get; set; }
        public string Description { get; set; }
    }
    
    private class Bar
    {
        public string Value { get; set; }
        public string Description { get; set; }
    }
}