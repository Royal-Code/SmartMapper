using System;
using System.Linq;
using FluentAssertions;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Builders;

public class AdapterOptionsBuilderTests
{
    [Fact]
    public void MapToMethod_Must_CreateAdapterSourceToMethodOptions_1X()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        var builder = new AdapterOptionsBuilder<Foo, Bar>(options);

        var toMethodBuilder = builder.MapToMethod();

        toMethodBuilder.Should().NotBeNull();
        
        options.GetSourceToMethodOptions()
            .Should().NotBeNull()
            .And.HaveCount(1);
    }
    
    [Fact]
    public void MapToMethod_Must_CreateAdapterSourceToMethodOptions_3X()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        var builder = new AdapterOptionsBuilder<Foo, Bar>(options);

        builder.MapToMethod();
        builder.MapToMethod();
        builder.MapToMethod();

        options.GetSourceToMethodOptions()
            .Should().NotBeNull()
            .And.HaveCount(3);
    }
    
    [Fact]
    public void MapToMethod_Must_AcceptMethodDelegate()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        var builder = new AdapterOptionsBuilder<Foo, Bar>(options);

        var toMethodBuilder = builder.MapToMethod(b => b.SomeMethod);

        toMethodBuilder.Should().NotBeNull();

        var methodOptions = options.GetSourceToMethodOptions().ToList();
        methodOptions
            .Should().NotBeNull()
            .And.HaveCount(1);
        
        methodOptions.First().Method.Should().NotBeNull();
    }
    
    [Fact]
    public void MapToMethod_MustThrown_When_MethodSelector_IsInvalid_Delegate()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        var builder = new AdapterOptionsBuilder<Foo, Bar>(options);

        Action act = () => builder.MapToMethod(b => b.InvalidDelegate);
        
        act.Should().Throw<InvalidMethodDelegateException>();
    }
    
    [Fact]
    public void MapToMethod_MustThrown_When_MethodSelector_IsInvalid_Property()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        var builder = new AdapterOptionsBuilder<Foo, Bar>(options);

        Action act = () => builder.MapToMethod(b => () => b.Value);
        
        act.Should().Throw<InvalidMethodDelegateException>();
    }

    [Fact]
    public void MapToMethod_MustThrown_When_MethodName_IsNullOrWhiteSpace()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        var builder = new AdapterOptionsBuilder<Foo, Bar>(options);

        Action actNull = () => builder.MapToMethod((string)null!);
        Action actWhiteSpace = () => builder.MapToMethod(" ");
        Action actEmpty = () => builder.MapToMethod(string.Empty);

        actNull.Should().Throw<InvalidMethodNameException>();
        actWhiteSpace.Should().Throw<InvalidMethodNameException>();
        actEmpty.Should().Throw<InvalidMethodNameException>();
    }

    [Fact]
    public void MapToMethod_MustThrown_When_MethodName_IsNotFound()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        var builder = new AdapterOptionsBuilder<Foo, Bar>(options);
        
        Action act = () => builder.MapToMethod("NonExistentMethod");
        
        act.Should().Throw<InvalidMethodNameException>();
    }
    
    [Fact]
    public void MapToMethod_Must_HasName_And_HasMethod_When_MethodName_IsFound()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        var builder = new AdapterOptionsBuilder<Foo, Bar>(options);
        
        var toMethodBuilder = builder.MapToMethod("SomeMethod");
        
        toMethodBuilder.Should().NotBeNull();
        
        var methodOptions = options.GetSourceToMethodOptions().ToList();
        methodOptions
            .Should().NotBeNull()
            .And.HaveCount(1);
        
        methodOptions.First().Method.Should().NotBeNull();
        methodOptions.First().MethodName.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void MapToMethod_Must_HasName_When_MethodName_IsFound()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        var builder = new AdapterOptionsBuilder<Foo, Bar>(options);
        
        var toMethodBuilder = builder.MapToMethod(nameof(Bar.DoSomething));
        
        toMethodBuilder.Should().NotBeNull();
        
        var methodOptions = options.GetSourceToMethodOptions().ToList();
        methodOptions
            .Should().NotBeNull()
            .And.HaveCount(1);
        
        methodOptions.First().Method.Should().BeNull();
        methodOptions.First().MethodName.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void Constructor_Must_Return_ConstructorBuilder()
    { 
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        var builder = new AdapterOptionsBuilder<Foo, Bar>(options);

        var constructorBuilder = builder.Constructor();

        constructorBuilder.Should().NotBeNull();
    }

    [Fact]
    public void Map_Must_Throw_When_SelectorIsNotAProperty()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        var builder = new AdapterOptionsBuilder<Foo, Bar>(options);
        
        Action act = () => builder.Map<Delegate>(b => () => b.SomeMethod);
        
        act.Should().Throw<InvalidPropertySelectorException>();
    }

    [Fact]
    public void Map_Must_Throw_When_NotFoundThePropertyByTheName()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        var builder = new AdapterOptionsBuilder<Foo, Bar>(options);
        
        Action act = () => builder.Map<string>("NotExistentProperty");
        
        act.Should().Throw<InvalidPropertyNameException>();
    }
    
    [Fact]
    public void Map_Must_ReturnThePropertyToPropertyBuilder()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        var builder = new AdapterOptionsBuilder<Foo, Bar>(options);
        
        var propertyBuilder = builder.Map<string>("Value");
        propertyBuilder.Should().NotBeNull();

        propertyBuilder = builder.Map(f => f.Value);
        propertyBuilder.Should().NotBeNull();
    }
    
    private class Foo
    {
        public string Value { get; set; }
        
        public Func<string> InvalidDelegate { get; set; }
        
        public void SomeMethod(string value) { }
    }
    
    private class Bar
    {
        public void SomeMethod(string value) { }
        public void DoSomething(string value) { }
        public void DoSomething(string value1, string value2) { }
        public Func<string> InvalidDelegate { get; set; }
        public string Value { get; set; }
    }
}