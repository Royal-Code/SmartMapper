
using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Builders;

public class AdapterParameterStrategyBuilderTetsts
{

    [Fact]
    public void CastValue_Must_SetOptions_And_ReturnTheBuilder()
    {
        var options = new AssignmentStrategyOptions<string>();
        var builder = new AdapterParameterStrategyBuilder<object, string>(options);

        var returned = builder.CastValue();

        options.Strategy.Should().Be(ValueAssignmentStrategy.Cast);
        returned.Should().NotBeNull();
    }

    [Fact]
    public void UseConverter_Must_SetOptions_And_SetAnnotation_And_ReturnTheBuilder()
    {
        var options = new AssignmentStrategyOptions<string>();
        var builder = new AdapterParameterStrategyBuilder<object, string>(options);

        var returned = builder.UseConverter(x => int.Parse(x));

        options.Strategy.Should().Be(ValueAssignmentStrategy.Convert);
        returned.Should().NotBeNull();

        options.FindAnnotation<ConvertOptions>().Should().NotBeNull();
    }

    [Fact]
    public void Adapt_Must_SetOptions_And_ReturnTheBuilder()
    {
        var options = new AssignmentStrategyOptions<string>();
        var builder = new AdapterParameterStrategyBuilder<object, string>(options);

        var returned = builder.Adapt();

        options.Strategy.Should().Be(ValueAssignmentStrategy.Adapt);
        returned.Should().NotBeNull();
    }

    [Fact]
    public void Select_Must_SetOptions_And_ReturnTheBuilder()
    {
        var options = new AssignmentStrategyOptions<string>();
        var builder = new AdapterParameterStrategyBuilder<object, string>(options);

        var returned = builder.Adapt();

        options.Strategy.Should().Be(ValueAssignmentStrategy.Adapt);
        returned.Should().NotBeNull();
    }

    [Fact]
    public void WithService_Must_SetOptions_And_SetAnnotation_And_ReturnTheBuilder()
    {
        var options = new AssignmentStrategyOptions<string>();
        var builder = new AdapterParameterStrategyBuilder<object, string>(options);

        var returned = builder.WithService<Processor, int>((p, v) => p.Parse(v));

        options.Strategy.Should().Be(ValueAssignmentStrategy.Processor);
        returned.Should().NotBeNull();

        options.FindAnnotation<ProcessorOptions>().Should().NotBeNull();
    }

    private class Processor
    {
        public int Parse(string value) => int.Parse(value);
    }
}
