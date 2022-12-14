using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Resolvers;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Activations;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Adapters;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Resolvers;

public class ActivationResolverTests
{
    [Fact]
    public void Resolve_Must_CreateSuccessResolution_ForDefaultConstructor()
    {
        // arrange
        var configs = ConfigurationBuilder.CreateDefault().Build();
        var adapterOptions = new AdapterOptions(typeof(Foo), typeof(Bar));
        var adapterContext = new AdapterContext(adapterOptions, configs);
        var activationRequest = adapterContext.CreateActivationRequest();

        var resolver = new ActivationResolver();

        // act
        var resolution = resolver.Resolve(activationRequest);

        // assert
        resolution.Resolved.Should().BeTrue();
    }

    private class Foo
    {
        public string Value { get; set; }
    }

    private class Bar
    {
        public string Value { get; set; }

    }

    private class Baz
    {
        public string? Value { get; }

        public Baz(string value)
        {
            Value = value;
        }

        public Baz()
        {

        }
    }

    private class Qux
    {
        public Foo? Foo { get; }

        public string? Value { get; }

        // valid
        public Qux(string value)
        {
            Value = value;
        }

        // invalid when set parameter types, valid otherwise
        public Qux()
        {

        }

        // invalid
        public Qux(Foo value)
        {
            Foo = value;
        }

        // invalid
        public Qux(Foo foo, string value)
        {
            Foo = foo;
            Value = value;
        }

        // invalid
        public Qux(string value, Foo? foo = null)
        {
            Foo = foo;
            Value = value;
        }
    }
}