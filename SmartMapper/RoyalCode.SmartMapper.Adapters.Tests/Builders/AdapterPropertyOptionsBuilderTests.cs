using FluentAssertions;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
