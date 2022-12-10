using FluentAssertions;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Options;

public class ToConstructorParameterOptionsTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void UseParameterName_Must_NotBeNullOrEmpty(string parameterName)
    {
        // arrange
        var propertyInfo = typeof(Foo).GetProperty(nameof(Foo.Value))!;
        var options = new ToConstructorParameterOptions(typeof(Bar), propertyInfo);

        // act
        Action act = () => options.UseParameterName(parameterName);
        
        // assert
        act.Should().Throw<InvalidParameterNameException>();
    }
    
    [Theory]
    [InlineData("value", true)]
    [InlineData("abc", false)]
    public void UseParameterName_Must_ValidateParameterName(string parameterName, bool isValid)
    {
        // arrange
        var propertyInfo = typeof(Foo).GetProperty(nameof(Foo.Value))!;
        var options = new ToConstructorParameterOptions(typeof(Bar), propertyInfo);
        
        var act = () => options.UseParameterName(parameterName);
        
        if (isValid)
            act.Should().NotThrow();
        else
            act.Should().Throw<InvalidParameterNameException>();
    }
 
#pragma warning disable CS8618
    
    private class Foo
    {
        public string Value { get; set; }
    }
    
    private class Bar
    {
        public Bar(string value){ }
    }
}