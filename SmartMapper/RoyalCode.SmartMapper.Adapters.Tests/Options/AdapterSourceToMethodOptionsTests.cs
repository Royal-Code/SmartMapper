using System.Linq;
using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Options;

public class AdapterSourceToMethodOptionsTests
{
    [Fact]
    public void GetPropertyToParameterOptions_Must_ReturnTheOptions()
    {
        var sourceToMethodOptions = new SourceToMethodOptions();

        var propertyInfo = typeof(Foo).GetProperty("Value")!;

        var options = sourceToMethodOptions.GetPropertyToParameterOptions(propertyInfo);
        options.Should().NotBeNull();
    }

    [Fact]
    public void GetPropertyToParameterOptions_Must_ReturnTheSameOptions()
    {
        var sourceToMethodOptions = new SourceToMethodOptions();

        var propertyInfo = typeof(Foo).GetProperty("Value")!;

        var options1 = sourceToMethodOptions.GetPropertyToParameterOptions(propertyInfo);
        var options2 = sourceToMethodOptions.GetPropertyToParameterOptions(propertyInfo);

        options1.Should().BeSameAs(options2);
    }
    
    [Fact]
    public void TryGetPropertyToParameterOptions_Must_ReturnTheOptions_When_Configured()
    {
        var sourceToMethodOptions = new SourceToMethodOptions();

        var propertyInfo = typeof(Foo).GetProperty("Value")!;

        var options = sourceToMethodOptions.GetPropertyToParameterOptions(propertyInfo);
        
        var found = sourceToMethodOptions.TryGetPropertyToParameterOptions(propertyInfo, out var options2);
        
        found.Should().BeTrue();
        options2.Should().BeSameAs(options);
    }
    
    [Fact]
    public void TryGetPropertyToParameterOptions_Must_ReturnFalse_WhenNotConfigured()
    {
        var sourceToMethodOptions = new SourceToMethodOptions();

        var propertyInfo = typeof(Foo).GetProperty("Value")!;

        var found = sourceToMethodOptions.TryGetPropertyToParameterOptions(propertyInfo, out var options);
        
        found.Should().BeFalse();
        options.Should().BeNull();
    }
    
    [Fact]
    public void ClearParameters_WhenNotConfigurated_Must_NotThrow()
    {
        var sourceToMethodOptions = new SourceToMethodOptions();

        sourceToMethodOptions.ClearParameters();
    }
    
    [Fact]
    public void ClearParameters_Must_ClearTheRelatedOptions()
    {
        var sourceToMethodOptions = new SourceToMethodOptions();

        var propertyInfo = typeof(Foo).GetProperty("Value")!;

        var options = sourceToMethodOptions.GetPropertyToParameterOptions(propertyInfo);
        options.Should().NotBeNull();

        PropertyOptions propertyOptions = new PropertyOptions(propertyInfo);
        propertyOptions.MappedToMethodParameter(options);
        propertyOptions.ResolutionOptions.Should().NotBeNull();
        
        sourceToMethodOptions.ClearParameters();
        propertyOptions.ResolutionOptions.Should().BeNull();
    }

    [Fact]
    public void ClearParameters_Must_ClearTheInternalCollections()
    {
            var sourceToMethodOptions = new SourceToMethodOptions();

        var propertyInfo = typeof(Foo).GetProperty("Value")!;

        var options = sourceToMethodOptions.GetPropertyToParameterOptions(propertyInfo);
        options.Should().NotBeNull();
        sourceToMethodOptions.AddPropertyToParameterSequence(options);

        PropertyOptions propertyOptions = new PropertyOptions(propertyInfo);
        propertyOptions.MappedToMethodParameter(options);
        propertyOptions.ResolutionOptions.Should().NotBeNull();
        
        sourceToMethodOptions.TryGetPropertyToParameterOptions(propertyInfo, out _).Should().BeTrue();
        sourceToMethodOptions.GetSelectedPropertyToParameterSequence().Should().HaveCount(1);
        
        sourceToMethodOptions.ClearParameters();
        sourceToMethodOptions.TryGetPropertyToParameterOptions(propertyInfo, out _).Should().BeFalse();
        sourceToMethodOptions.GetSelectedPropertyToParameterSequence().Should().HaveCount(0);
    }

    [Fact]
    public void GetSelectedPropertyToParameterSequence_Must_ReturnEmpty_When_NotConfigurated()
    {
        var sourceToMethodOptions = new SourceToMethodOptions();

        sourceToMethodOptions.GetSelectedPropertyToParameterSequence().Should().BeEmpty();
    }

    [Fact]
    public void GetSelectedPropertyToParameterSequence_Must_ReturnTheParametersInOrder()
    {
        var sourceToMethodOptions = new SourceToMethodOptions();

        var propertyInfo1 = typeof(Foo).GetProperty("Value")!;
        var options1 = sourceToMethodOptions.GetPropertyToParameterOptions(propertyInfo1);
        sourceToMethodOptions.AddPropertyToParameterSequence(options1);

        var propertyInfo2 = typeof(Foo).GetProperty("Description")!;
        var options2 = sourceToMethodOptions.GetPropertyToParameterOptions(propertyInfo2);
        sourceToMethodOptions.AddPropertyToParameterSequence(options2);

        var sequence = sourceToMethodOptions.GetSelectedPropertyToParameterSequence();
        sequence.Should().HaveCount(2);
        sequence[0].Should().BeSameAs(options1);
        sequence[1].Should().BeSameAs(options2);
    }
    
    private class Foo
    {
        public string Value { get; set; }
        public string Description { get; set; }
    }
}