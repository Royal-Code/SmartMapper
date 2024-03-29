﻿using FluentAssertions;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;
using RoyalCode.SmartMapper.Infrastructure.Core;
using System;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Builders;

public class AdapterProTpertyOptionsBuilderTests
{
    [Fact]
    public void To_Must_Throw_When_SelectorIsNotAProperty()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);

        var act = () => builder.To<Delegate>(x => x.DoSomething);

        act.Should().Throw<InvalidPropertySelectorException>();
    }

    [Fact]
    public void To_Must_ReturnTheOptionsBuilder_When_SelectorAValidProperty()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);

        var nextBuilder = builder.To(x => x.OtherValue);

        nextBuilder.Should().NotBeNull();
    }

    [Fact]
    public void To_Must_ConfigurePropertOption_With_MappedToProperty_WhenSelectorIsValid()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);
        
        var nextBuilder = builder.To(x => x.OtherValue);
        nextBuilder.Should().NotBeNull();

        propertyOptions.ResolutionStatus.Should().Be(ResolutionStatus.MappedToProperty);
        propertyOptions.ResolutionOptions.Should().NotBeNull().And.BeOfType<ToPropertyOptions>();
        
        var resolutionOptions = (ToPropertyOptions)propertyOptions.ResolutionOptions!;
        resolutionOptions.ResolvedProperty.Should().NotBeNull().And.BeSameAs(propertyOptions);
    }

    [Fact]
    public void To_Must_Throw_When_PropertyNameNotFound()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);

        var act = () => builder.To<string>("InvalidName");
        
        act.Should().Throw<InvalidPropertyNameException>();
    }

    [Fact]
    public void To_Must_Throw_When_PropertyTypeIsNotEqualToInformedType()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);
        
        var act = () => builder.To<int>("OtherValue");
        
        act.Should().Throw<InvalidPropertyTypeException>();
    }

    [Fact]
    public void To_Must_ReturnTheOptionsBuilder_When_PropertyNameIsValid()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);

        var nextBuilder = builder.To<string>("OtherValue");
        
        nextBuilder.Should().NotBeNull();
    }

    [Fact]
    public void To_Must_ConfigurePropertyOption_With_MappedToProperty_PropertyNameIsValid()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);

        var nextBuilder = builder.To<string>("OtherValue");
        nextBuilder.Should().NotBeNull();

        propertyOptions.ResolutionStatus.Should().Be(ResolutionStatus.MappedToProperty);
        propertyOptions.ResolutionOptions.Should().NotBeNull().And.BeOfType<ToPropertyOptions>();
        
        var resolutionOptions = (ToPropertyOptions)propertyOptions.ResolutionOptions!;
        resolutionOptions.ResolvedProperty.Should().NotBeNull().And.BeSameAs(propertyOptions);
    }

    [Fact]
    public void ToConstructor_Must_ReturnTheBuilder()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);

        var nextBuilder = builder.ToConstructor();

        nextBuilder.Should().NotBeNull();
    }

    [Fact]
    public void ToConstructor_Must_ConfigurePropertyOptions_With_MappedToConstructor()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);

        var nextBuilder = builder.ToConstructor();
        nextBuilder.Should().NotBeNull();

        propertyOptions.ResolutionStatus.Should().Be(ResolutionStatus.MappedToConstructor);
        propertyOptions.ResolutionOptions.Should().NotBeNull().And.BeOfType<ToConstructorOptions>();
    }

    [Fact]
    public void ToMethod_Must_ReturnTheBuilder()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);

        var nextBuilder = builder.ToMethod();
        nextBuilder.Should().NotBeNull();
    }

    [Fact]
    public void ToMethod_With_Delegate_Must_ReturnTheBuilder()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);

        var nextBuilder = builder.ToMethod(x => x.DoSomething);
        nextBuilder.Should().NotBeNull();
    }

    [Fact]
    public void ToMethod_Must_ConfigurePropertyOptions_With_MappedToMethod()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);

        var nextBuilder = builder.ToMethod();
        nextBuilder.Should().NotBeNull();

        propertyOptions.ResolutionStatus.Should().Be(ResolutionStatus.MappedToMethod);
        propertyOptions.ResolutionOptions.Should().NotBeNull().And.BeOfType<ToMethodOptions>();
    }

    [Fact]
    public void ToMethod_With_Delegate_Must_Throw_When_InvalidDelegate()
    {
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(typeof(Foo).GetProperty("Value")!);
        var builder = new AdapterPropertyOptionsBuilder<Foo, Bar, string>(adapterOptions, propertyOptions);

        var act = () => builder.ToMethod(x => string.IsNullOrEmpty);
        
        act.Should().Throw<InvalidMethodDelegateException>();
    }

#pragma warning disable CS8618

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
