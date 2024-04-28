using RoyalCode.SmartMapper.Adapters.Configurations.Internal;
using RoyalCode.SmartMapper.Adapters.Options;

namespace RoyalCode.SmartMapper.Tests.Mappings.Adapters;

public sealed class ConstrutorMappings
{
    /// <summary>
    /// Simple constructor parameter mapping.
    /// <br/>
    /// Source has a property to be mapped to the constructor parameter.
    /// </summary>
    public void Single_Simple()
    {
        // Arrange
        var options = AdapterOptions.For<SingleSource, SingleTarget>();
        var builder = new AdapterOptionsBuilder<SingleSource, SingleTarget>(options);
        
        // Act
        builder.Constructor().Parameters(b =>
        {
            b.Parameter(s => s.Name);
        });
    }

    /// <summary>
    /// Simple constructor parameter mapping.
    /// <br/>
    /// Source has a complex type property, where the inner property are mapped to the constructor parameter.
    /// </summary>
    public void Single_Complex()
    {
        // Arrange
        var options = AdapterOptions.For<SingleComplexSource, SingleComplexTarget>();
        var builder = new AdapterOptionsBuilder<SingleComplexSource, SingleComplexTarget>(options);
        
        // Act
        builder.Map(s => s.Value).ToConstructor();
    }
}

#region Single_Simple

file class SingleSource
{
    public string Name { get; set; }
}

file class SingleTarget
{
    public SingleTarget(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
    
    public Guid Id { get; }
    
    public string Name { get; }
}
#endregion

#region Single_Complex

file class SingleComplexSource
{
    public ComplexValue Value { get; set; }
}

file class ComplexValue
{
    public string Name { get; set; }
}

file class SingleComplexTarget
{
    public SingleComplexTarget(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
    
    public Guid Id { get; }
    
    public string Name { get; }
}

#endregion
