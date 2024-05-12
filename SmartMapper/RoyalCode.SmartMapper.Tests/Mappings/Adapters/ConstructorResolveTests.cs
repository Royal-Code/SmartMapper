using RoyalCode.SmartMapper.Adapters.Resolutions.Contexts;
using RoyalCode.SmartMapper.Core.Configurations;

namespace RoyalCode.SmartMapper.Tests.Mappings.Adapters;

public class ConstructorResolveTests
{
    private void Prepare<TSource, TTarget>(
        out MapperConfigurations configurations,
        out ActivationContext activationContext)
    {
        Util.PrepareAdapter<TSource, TTarget>(out configurations, out activationContext);
    }
    
    [Fact]
    public void Constructor_Resolve_Empty_FromEmpty()
    {
        // Arrange
        Prepare<EmptySource, EmptyTarget>(out var configuration, out var activationContext);
        
        // Act
        var resolution = activationContext.CreateResolution(configuration);
        
        // Assert
        Assert.NotNull(resolution);
        Assert.True(resolution.Resolved);
    }

    [Fact]
    public void Constructor_Resolve_Empty_FromEmptyOrOne()
    {
        // Arrange
        Prepare<EmptySource, EmptyAndOneParameterTarget>(out var configuration, out var activationContext);
        
        // Act
        var resolution = activationContext.CreateResolution(configuration);
        
        // Assert
        Assert.NotNull(resolution);
        Assert.True(resolution.Resolved);
    }
    
    [Fact]
    public void Constructor_Resolve_OneParameter_FromOneParameter()
    {
        // Arrange
        Prepare<OneParameterSource, EmptyAndOneParameterTarget>(out var configuration, out var activationContext);
        
        // Act
        var resolution = activationContext.CreateResolution(configuration);
        
        // Assert
        Assert.NotNull(resolution);
        Assert.True(resolution.Resolved);
        Assert.NotNull(resolution.ConstructorResolution);
        Assert.Single(resolution.ConstructorResolution.ParameterResolution);
    }
}

#pragma warning disable
// ReSharper disable ClassNeverInstantiated.Local

file class EmptySource { }
file class EmptyTarget { }
file class OneParameterSource { public int Value { get; set; } }
file class EmptyAndOneParameterTarget
{
    public EmptyAndOneParameterTarget(int value) { }
    public EmptyAndOneParameterTarget() { }
    public int Value { get; set; }
}
