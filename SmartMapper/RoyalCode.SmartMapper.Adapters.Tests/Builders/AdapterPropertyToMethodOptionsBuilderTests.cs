using FluentAssertions;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Builders;

public class AdapterPropertyToMethodOptionsBuilderTests
{
    [Theory]
    [InlineData("CallMe", false)]
    [InlineData("", false)]
    [InlineData("DoSomething", true)]
    public void UseMethod_Must_HaveAValidMethodName(string methodName, bool isValid)
    {
        // arrange
        var propertyOptions = new PropertyOptions(typeof(Foo).GetProperty(nameof(Foo.Value))!);
        var methodOptions = new MethodOptions(typeof(Bar));
        var toMethod = new ToMethodOptions(propertyOptions, methodOptions);
        propertyOptions.MappedToMethod(toMethod);
        
        var builder = new AdapterPropertyToMethodOptionsBuilder<Bar, string>(toMethod);
        
        // act
        var act = () => builder.UseMethod(methodName);

        // assert
        if (isValid)
            act.Should().NotThrow();
        else
            act.Should().Throw<InvalidMethodNameException>();
    }

    [Theory]
    [InlineData("DoSomething", true)]
    [InlineData("OtherMethod", false)]
    public void UseMethod_Must_AssignTheMethod_When_HasOnlyOneMethodForTheName(string methodName, bool onlyOneMethod)
    {
        // arrange
        var propertyOptions = new PropertyOptions(typeof(Foo).GetProperty(nameof(Foo.Value))!);
        var methodOptions = new MethodOptions(typeof(Bar));
        var toMethod = new ToMethodOptions(propertyOptions, methodOptions);
        propertyOptions.MappedToMethod(toMethod);
        
        var builder = new AdapterPropertyToMethodOptionsBuilder<Bar, string>(toMethod);
        
        // act
        builder.UseMethod(methodName);

        // assert
        if (onlyOneMethod)
            methodOptions.Method.Should().NotBeNull();
        else
            methodOptions.Method.Should().BeNull();
    }
    
    [Fact]
    public void UseMethod_Must_AcceptValidMethodDelegate()
    {
        // arrange
        var propertyOptions = new PropertyOptions(typeof(Foo).GetProperty(nameof(Foo.Value))!);
        var methodOptions = new MethodOptions(typeof(Bar));
        var toMethod = new ToMethodOptions(propertyOptions, methodOptions);
        propertyOptions.MappedToMethod(toMethod);
        
        var builder = new AdapterPropertyToMethodOptionsBuilder<Bar, string>(toMethod);

        // act
        var act = () => builder.UseMethod(bar => bar.DoSomething);

        // assert
        act.Should().NotThrow();
        methodOptions.Method.Should().NotBeNull();
    }

    [Fact]
    public void Value_Must_ExecuteTheAction()
    {
        // arrange
        var propertyOptions = new PropertyOptions(typeof(Foo).GetProperty(nameof(Foo.Value))!);
        var methodOptions = new MethodOptions(typeof(Bar));
        var toMethod = new ToMethodOptions(propertyOptions, methodOptions);
        propertyOptions.MappedToMethod(toMethod);

        var builder = new AdapterPropertyToMethodOptionsBuilder<Bar, string>(toMethod);

        // act
        bool executed = false;
        builder.Value(b => executed = true);
        
        // assert
        executed.Should().BeTrue();
    }

    [Fact]
    public void Value_Must_ConfigureStrategy_ToValue()
    {
        // arrange
        var propertyOptions = new PropertyOptions(typeof(Foo).GetProperty(nameof(Foo.Value))!);
        var methodOptions = new MethodOptions(typeof(Bar));
        var toMethod = new ToMethodOptions(propertyOptions, methodOptions);
        propertyOptions.MappedToMethod(toMethod);

        var builder = new AdapterPropertyToMethodOptionsBuilder<Bar, string>(toMethod);

        // act
        builder.Value(_ => { });

        // assert
        toMethod.Strategy.Should().Be(ToParametersStrategy.Value);
    }
    
    [Fact]
    public void Parameters_Must_ExecuteTheAction()
    {
        // arrange
        var propertyOptions = new PropertyOptions(typeof(Foo).GetProperty(nameof(Foo.Value))!);
        var methodOptions = new MethodOptions(typeof(Bar));
        var toMethod = new ToMethodOptions(propertyOptions, methodOptions);
        propertyOptions.MappedToMethod(toMethod);

        var builder = new AdapterPropertyToMethodOptionsBuilder<Bar, string>(toMethod);

        // act
        bool executed = false;
        builder.Parameters(b => executed = true);
        
        // assert
        executed.Should().BeTrue();
    }
    
    [Fact]
    public void Parameters_Must_ConfigureStrategy_ToInnerProperties()
    {
        // arrange
        var propertyOptions = new PropertyOptions(typeof(Foo).GetProperty(nameof(Foo.Value))!);
        var methodOptions = new MethodOptions(typeof(Bar));
        var toMethod = new ToMethodOptions(propertyOptions, methodOptions);
        propertyOptions.MappedToMethod(toMethod);

        var builder = new AdapterPropertyToMethodOptionsBuilder<Bar, string>(toMethod);

        // act
        builder.Parameters(_ => { });
        
        // assert
        toMethod.Strategy.Should().Be(ToParametersStrategy.InnerProperties);
    }
    
    private class Foo
    {
        public string Value { get; set; }
    }

    private class Bar
    {
        public void DoSomething(string input) { }
        
        public void OtherMethod() { }
        
        public void OtherMethod(int value) { }
    }
}