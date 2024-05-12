using RoyalCode.SmartMapper.Adapters.Configurations;
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
    
    private void Configure<TSource, TTarget>(
        Action<IAdapterOptionsBuilder<TSource, TTarget>> configure,
        out MapperConfigurations configurations,
        out ActivationContext activationContext)
    {
        Util.PrepareAdapter(configure, out configurations, out activationContext);
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
    
    [Fact]
    public void Constructor_Resolve_TwoParameter_FromTwoParameter()
    {
        // Arrange
        Prepare<TwoParametersSource, EmptyAndOneAndTwoParametersTarget>(out var configuration, out var activationContext);
        
        // Act
        var resolution = activationContext.CreateResolution(configuration);
        
        // Assert
        Assert.NotNull(resolution);
        Assert.True(resolution.Resolved);
        Assert.NotNull(resolution.ConstructorResolution);
        Assert.Equal(2, resolution.ConstructorResolution.ParameterResolution.Count());
    }

    [Fact]
    public void Constructor_ConfigureAndResolve_TwoParameter_FromOneParameter()
    {
        // Arrange
        Configure<OneParameterSource, EmptyAndOneAndTwoParametersTarget>(
            x => x.Constructor().Parameters(p => p.Parameter(o => o.Value, "value1")),
            out var configuration, out var activationContext);
        
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

file class TwoParametersSource { public int Value1 { get; set; } public int Value2 { get; set; } }
file class EmptyAndOneAndTwoParametersTarget
{
    public EmptyAndOneAndTwoParametersTarget(int value1, int value2) { }
    public EmptyAndOneAndTwoParametersTarget(int value1) { }
    public EmptyAndOneAndTwoParametersTarget() { }
    public int Value1 { get; set; }
    public int Value2 { get; set; }
}

file class OtherNamesSource { public int Id { get; set; } public int Quantity { get; set; } }