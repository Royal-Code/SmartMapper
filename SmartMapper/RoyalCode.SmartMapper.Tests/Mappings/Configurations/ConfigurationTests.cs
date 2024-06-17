using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Core.Gererators;

namespace RoyalCode.SmartMapper.Tests.Mappings.Configurations;

public class ConfigurationTests
{
    [Fact]
    public void MapperOptions_Must_CreateAdapterOptions()
    {
        var mapperOptions = new MapperOptions();
        
        var adapterOptions = mapperOptions.GetAdapterOptions<EmptyDto, EmptyEntity>();
        
        Assert.NotNull(adapterOptions);
    }
    
    [Fact]
    public void Must_CreateMapperConfigurations()
    {
        var mapperOptions = new MapperOptions();
        var expressionGenerator = new ExpressionGenerator();
        var configuration = new MapperConfigurations(mapperOptions, expressionGenerator);

        Assert.NotNull(configuration);
        Assert.NotNull(configuration.Discovery);
    }
}

file class EmptyDto { }
file class EmptyEntity { }