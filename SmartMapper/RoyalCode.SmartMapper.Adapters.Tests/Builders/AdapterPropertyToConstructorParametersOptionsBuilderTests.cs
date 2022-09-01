using RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RoyalCode.SmartMapper.Adapters.Tests.Builders;

public class AdapterPropertyToConstructorParametersOptionsBuilderTests
{

    public void Ignore_Must_ConfigureThePropertyToBeIgnored()
    {
        var sourceOptions = new SourceOptions(typeof(Bar));
        var constructorOptions = new ConstructorOptions(typeof(Qux));
        
        var builder = new AdapterPropertyToConstructorParametersOptionsBuilder<Bar>(sourceOptions, constructorOptions);
        
        builder.Ignore(b => b.OtherValue);
        
        // TODO: Implement test
    }
    
    private class Foo
    {
        public Bar Value { get; set; }
    }

    private class Bar
    {
        public string Value { get; set; }
        
        public string OtherValue { get; set; }
    }
    
    private class Qux
    {
        public string SomeValue { get; }

        public Qux(string someValue)
        {
            SomeValue = someValue;
        }
    }
}