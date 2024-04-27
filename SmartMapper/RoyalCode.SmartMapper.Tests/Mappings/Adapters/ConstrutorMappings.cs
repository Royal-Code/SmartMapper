using RoyalCode.SmartMapper.Adapters.Configurations.Internal;
using RoyalCode.SmartMapper.Adapters.Options;

namespace RoyalCode.SmartMapper.Tests.Mappings.Adapters;

public sealed class ConstrutorMappings
{
    public void Single_Simple()
    {
        var options = AdapterOptions.For<SingleSource, SingleTarget>();
        var builder = new AdapterOptionsBuilder<SingleSource, SingleTarget>(options);
        builder.Constructor().Parameters(b =>
        {
            b.Parameter(s => s.Name);
        });
    }
}

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
