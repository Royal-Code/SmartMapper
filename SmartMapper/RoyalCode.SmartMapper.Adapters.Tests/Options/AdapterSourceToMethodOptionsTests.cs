using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Options;

public class AdapterSourceToMethodOptionsTests
{
    [Fact]
    public void GetPropertyToParameterOptions_Must_ReturnTheOptions()
    {
        var sourceToMethodOptions = new AdapterSourceToMethodOptions();

        var propertyInfo = typeof(Foo).GetProperty("Value")!;

        var options = sourceToMethodOptions.GetPropertyToParameterOptions(propertyInfo);
        options.Should().NotBeNull();
    }

    [Fact]
    public void GetPropertyToParameterOptions_Must_ReturnTheSameOptions()
    {
        var sourceToMethodOptions = new AdapterSourceToMethodOptions();

        var propertyInfo = typeof(Foo).GetProperty("Value")!;

        var options1 = sourceToMethodOptions.GetPropertyToParameterOptions(propertyInfo);
        var options2 = sourceToMethodOptions.GetPropertyToParameterOptions(propertyInfo);

        options1.Should().BeSameAs(options2);
    }
    
    [Fact]
    public void ClearParameters_WhenNotConfigurated_Must_NotThrow()
    {
        var sourceToMethodOptions = new AdapterSourceToMethodOptions();

        sourceToMethodOptions.ClearParameters();
    }
    
    [Fact]
    public void ClearParameters_Must_ClearTheRelatedOptions()
    {
        var sourceToMethodOptions = new AdapterSourceToMethodOptions();

        var propertyInfo = typeof(Foo).GetProperty("Value")!;

        var options = sourceToMethodOptions.GetPropertyToParameterOptions(propertyInfo);
        options.Should().NotBeNull();

        PropertyOptions propertyOptions = new PropertyOptions(propertyInfo);
        propertyOptions.MappedToMethodParameter(options);
        propertyOptions.ResolutionOptions.Should().NotBeNull();
        
        sourceToMethodOptions.ClearParameters();
        propertyOptions.ResolutionOptions.Should().BeNull();
    }

    private class Foo
    {
        public string Value { get; set; }
    }
}