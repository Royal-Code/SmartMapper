using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Resolutions.Contexts;
using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Core.Gererators;
using RoyalCode.SmartMapper.Core.Options;

namespace RoyalCode.SmartMapper.Tests.Mappings.Adapters;

public class ConstructorResolveTests
{
    [Fact]
    public void Constructor_Resolve_Empty()
    {
        // Arrange
        var mapperOptions = new MapperOptions();
        var expressionGenerator = new ExpressionGenerator();
        var configuration = new MapperConfigurations(mapperOptions, expressionGenerator);
        var options = mapperOptions.GetAdapterOptions<EmptySource, EmptyTarget>();
        var adapterContext = AdapterContext.Create(options);
        var activationContext = ActivationContext.Create(adapterContext);
        
        // Act
        var resolution = activationContext.CreateResolution(configuration);
        
        // Assert
        Assert.NotNull(resolution);
        Assert.True(resolution.Resolved);
    }
}

file class EmptySource { }
file class EmptyTarget { }