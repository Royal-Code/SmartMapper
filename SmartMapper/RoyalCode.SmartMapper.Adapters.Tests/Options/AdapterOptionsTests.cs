using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Options;

public class AdapterOptionsTests
{
    [Fact]
    public void CreateSourceToMethodOptions_Must_CreateNewOptionsEachCall()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        var methodOptions1 = options.CreateSourceToMethodOptions();
        var methodOptions2 = options.CreateSourceToMethodOptions();

        methodOptions1.Should().NotBeNull();
        methodOptions2.Should().NotBeNull().And.NotBeSameAs(methodOptions1);
    }
    
    [Fact]
    public void GetSourceToMethodOptions_Must_ReturnOptions_When_Options()
    {
        var options = new AdapterOptions(typeof(Foo), typeof(Bar));
        options.CreateSourceToMethodOptions();
        options.CreateSourceToMethodOptions();
        options.CreateSourceToMethodOptions();
        
        var result = options.GetSourceToMethodOptions();
        
        result.Should().NotBeNull().And.HaveCount(3);
    }
    
    private class Foo : Quux
    {
        public string Value { get; set; }
    }

    private class Bar
    {
        public string Value { get; set; }
    }

    private class Quux
    {
        public string Other { get; set; }
    }
}