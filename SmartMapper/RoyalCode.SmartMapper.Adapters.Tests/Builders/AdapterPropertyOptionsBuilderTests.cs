using FluentAssertions;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;
using RoyalCode.SmartMapper.Infrastructure.Core;
using System;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Builders;

public class AdapterProTpertyOptionsBuilderTests
{
    [Fact]
    public void To_Must_Throw_When_SelectorIsNotAProperty()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);

        var act = () => builder.To<Delegate>(x => x.DoSomething);

        act.Should().Throw<InvalidPropertySelectorException>();
    }

    [Fact]
    public void To_Must_ReturnTheOptionsBuilder_When_SelectorAValidProperty()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);

        var nextBuilder = builder.To(x => x.OtherValue);

        nextBuilder.Should().NotBeNull();
    }

    [Fact]
    public void To_Must_ConfigurePropertOption_With_MappedToProperty_WhenSelectorIsValid()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);
        
        var nextBuilder = builder.To(x => x.OtherValue);
        nextBuilder.Should().NotBeNull();

        propertyOptions.ResolutionStatus.Should().Be(ResolutionStatus.MappedToProperty);
        // TODO: checar o propertyOptions.ResolutionOptions para ser algo PropertyToProperty.
    }

    [Fact]
    public void To_Must_Throw_When_PropertyNameNotFound()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);

        var act = () => builder.To<string>("InvalidName");
        
        act.Should().Throw<InvalidPropertyNameException>();
    }

    [Fact]
    public void To_Must_ReturnTheOptionsBuilder_When_PropertyNameIsValid()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);

        var nextBuilder = builder.To<string>("OtherValue");
        
        nextBuilder.Should().NotBeNull();
    }

    [Fact]
    public void To_Must_ConfigurePropertyOption_With_MappedToProperty_PropertyNameIsValid()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);

        var nextBuilder = builder.To<string>("OtherValue");
        nextBuilder.Should().NotBeNull();

        propertyOptions.ResolutionStatus.Should().Be(ResolutionStatus.MappedToProperty);
        // TODO: checar o propertyOptions.ResolutionOptions para ser algo PropertyToProperty.
    }

    [Fact]
    public void ToConstructor_Must_ReturnTheBuilder()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);

        var nextBuilder = builder.ToConstructor();

        nextBuilder.Should().NotBeNull();
    }

    [Fact]
    public void ToConstructor_Must_ConfigurePropertyOptions_With_MapInternalProperties()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);

        var nextBuilder = builder.ToConstructor();
        nextBuilder.Should().NotBeNull();

        //propertyOptions.ResolutionStatus.Should().Be(ResolutionStatus.MapInternalProperties);
    }

    [Fact]
    public void ToMethod_Must_ReturnTheBuilder()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);

        var nextBuilder = builder.ToMethod();
        nextBuilder.Should().NotBeNull();
    }

    [Fact]
    public void ToMethod_With_Delegate_Must_ReturnTheBuilder()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);

        var nextBuilder = builder.ToMethod(x => x.DoSomething);
        nextBuilder.Should().NotBeNull();
    }

    [Fact]
    public void ToMethod_Must_ConfigurePropertyOptions_With_MapInternalProperties()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);

        var nextBuilder = builder.ToMethod();
        nextBuilder.Should().NotBeNull();

        //propertyOptions.ResolutionStatus.Should().Be(ResolutionStatus.MapInternalProperties);
    }

    [Fact]
    public void ToMethod_With_Delegate_Must_Throw_When_InvalidDelegate()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);

        var act = () => builder.ToMethod(x => string.IsNullOrEmpty);
        
        act.Should().Throw<InvalidMethodDelegateException>();
    }

    private class Foo
    {
        public string Value { get; set; }
    }

    private class Bar
    {
        public string OtherValue { get; set; }

        public string DoSomething()
        {
            return string.Empty;
        }
    }
}
