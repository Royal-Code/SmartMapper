using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Options;

public class AssignmentStrategyOptionsTests
{
    [Fact]
    public void UseConvert_Must_CreateAConverterOptions()
    {
        var options = new AssignmentStrategyOptions<Foo>();
        options.UseConvert(foo => foo.ToString() ?? string.Empty);

        var convertOptions = options.FindAnnotation<ConvertOptions>();
        convertOptions.Should().NotBeNull();
        convertOptions.SourceValueType.Should().Be<Foo>();
        convertOptions.TargetValueType.Should().Be<string>();
    }
    
    [Fact]
    public void UseProcessor_Must_CreateAProcessorOptions()
    {
        var options = new AssignmentStrategyOptions<Foo>();
        options.UseProcessor<FooProcessor, string>((processor, foo) => processor.Process(foo));

        var processorOptions = options.FindAnnotation<ProcessorOptions>();
        processorOptions.Should().NotBeNull();
        processorOptions.SourceValueType.Should().Be<Foo>();
        processorOptions.TargetValueType.Should().Be<string>();
        processorOptions.ServiceType.Should().Be<FooProcessor>();
    }

    private class Foo { }

    private class FooProcessor
    {
        public string Process(Foo foo)
        {
            return foo.ToString() ?? string.Empty;
        }
    }
}