using FluentAssertions;
using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Builders;

public class AdapterSourceToMethodOptionsBuilderTests
{
    [Theory]
    [InlineData("CallMe", false)]
    [InlineData("", false)]
    [InlineData("DoSomething", true)]
    public void UseMethod_Must_HaveAValidMethodName(string methodName, bool isValid)
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = adapterOptions.CreateSourceToMethodOptions();

        var builder = new AdapterSourceToMethodOptionsBuilder<Foo, Bar>(adapterOptions, methodOptions);

        var act = () => builder.UseMethod(methodName);

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
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = adapterOptions.CreateSourceToMethodOptions();

        var builder = new AdapterSourceToMethodOptionsBuilder<Foo, Bar>(adapterOptions, methodOptions);

        builder.UseMethod(methodName);

        if (onlyOneMethod)
            methodOptions.MethodOptions.Method.Should().NotBeNull();
        else
            methodOptions.MethodOptions.Method.Should().BeNull();
    }
    
    [Fact]
    public void UseMethod_Must_AcceptValidMethodDelegate()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = adapterOptions.CreateSourceToMethodOptions();

        var builder = new AdapterSourceToMethodOptionsBuilder<Foo, Bar>(adapterOptions, methodOptions);

        var act = () => builder.UseMethod(bar => bar.DoSomething);

        act.Should().NotThrow();
        methodOptions.MethodOptions.Method.Should().NotBeNull();
    }
    
    [Fact]
    public void Parameters_Must_CallTheConfigureAction()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = adapterOptions.CreateSourceToMethodOptions();

        var builder = new AdapterSourceToMethodOptionsBuilder<Foo, Bar>(adapterOptions, methodOptions);

        bool called = false;
        IAdapterSourceToMethodParametersOptionsBuilder<Foo>? parametersBuilder = null;
        builder.Parameters(b =>
        {
            called = true;
            parametersBuilder = b;
        });
        
        called.Should().BeTrue();
        parametersBuilder.Should().NotBeNull();
    }
    
    [Fact]
    public void Parameters_Must_SetParametersStrategy_With_SelectedParameters()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = adapterOptions.CreateSourceToMethodOptions();
            
        var builder = new AdapterSourceToMethodOptionsBuilder<Foo, Bar>(adapterOptions, methodOptions);

        builder.Parameters(_ => { });
        
        methodOptions.Strategy.Should().Be(SourceToMethodStrategy.SelectedParameters);
    }
    
    [Fact]
    public void AllProperties_Must_CallTheConfigureAction()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = adapterOptions.CreateSourceToMethodOptions();

        var builder = new AdapterSourceToMethodOptionsBuilder<Foo, Bar>(adapterOptions, methodOptions);

        bool called = false;
        IAdapterSourceToMethodPropertiesOptionsBuilder<Foo>? allPropertiesBuilder = null;
        builder.AllProperties(b =>
        {
            called = true;
            allPropertiesBuilder = b;
        });
        
        called.Should().BeTrue();
        allPropertiesBuilder.Should().NotBeNull();
    }
    
    [Fact]
    public void AllProperties_Must_SetParametersStrategy_With_AllParameters()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions = adapterOptions.CreateSourceToMethodOptions();
            
        var builder = new AdapterSourceToMethodOptionsBuilder<Foo, Bar>(adapterOptions, methodOptions);

        builder.AllProperties(_ => { });
        
        methodOptions.Strategy.Should().Be(SourceToMethodStrategy.AllParameters);
    }

    private class Foo
    {
        public string Value { get; set; }
    }

    private class Bar
    {
        public void DoSomething()
        {
        }

        public void OtherMethod(string param)
        {
        }

        public void OtherMethod(int param)
        {
        }
    }
}