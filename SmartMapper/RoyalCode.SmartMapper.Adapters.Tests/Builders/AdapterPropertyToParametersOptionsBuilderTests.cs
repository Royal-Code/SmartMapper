using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

namespace RoyalCode.SmartMapper.Adapters.Tests.Builders;

public class AdapterPropertyToParametersOptionsBuilderTests
{

    public void Ignore_Must_ConfigureThePropertyToBeIgnored()
    {
        var constructorOptions = new ConstructorOptions(typeof(Qux));
        var propertyToConstructorOptions = new PropertyToConstructorOptions(typeof(Bar), constructorOptions);
        var builder = new AdapterPropertyToParametersOptionsBuilder<Foo, Bar>(propertyToConstructorOptions);
        
        builder.Ignore(b => b.OtherValue);
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