using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Options;

public class MethodOptionsTests
{
    [Fact]
    public void GetParameterOptions_Must_ReturnTheOptions()
    {
        var methodOptions = new MethodOptions(typeof(Bar));
        var propertyInfo = typeof(Foo).GetProperty("Value")!;

        var options = methodOptions.GetParameterOptions(propertyInfo);
        options.Should().NotBeNull();
    }

    [Fact]
    public void GetParameterOptions_Must_ReturnTheSameOptions()
    {
        var methodOptions = new MethodOptions(typeof(Bar));
        var propertyInfo = typeof(Foo).GetProperty("Value")!;

        var options1 = methodOptions.GetParameterOptions(propertyInfo);
        var options2 = methodOptions.GetParameterOptions(propertyInfo);

        options1.Should().BeSameAs(options2);
    }
    
    [Fact]
    public void TryGetParameterOptions_Must_ReturnTheOptions_When_Configured()
    {
        var methodOptions = new MethodOptions(typeof(Bar));
        var propertyInfo = typeof(Foo).GetProperty("Value")!;
        var options = methodOptions.GetParameterOptions(propertyInfo);
        
        var found = methodOptions.TryGetParameterOptions(propertyInfo, out var options2);
        
        found.Should().BeTrue();
        options2.Should().BeSameAs(options);
    }
    
    [Fact]
    public void TryGetPropertyToParameterOptions_Must_ReturnFalse_WhenNotConfigured()
    {
        var methodOptions = new MethodOptions(typeof(Bar));
        var propertyInfo = typeof(Foo).GetProperty("Value")!;

        var found = methodOptions.TryGetParameterOptions(propertyInfo, out var options);
        
        found.Should().BeFalse();
        options.Should().BeNull();
    }
    
    // TODO: Testar o método WithMethodName
    
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