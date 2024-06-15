using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Mapping.Builders;
using RoyalCode.SmartMapper.Mapping.Resolvers;

namespace RoyalCode.SmartMapper.Tests.Mappings.ToConstructors;

public class ConstructorResolveTests
{
    private void Prepare<TSource, TTarget>(
        out MapperConfigurations configurations,
        out ActivationResolver activationResolver)
    {
        Util.PrepareAdapter<TSource, TTarget>(out configurations, out activationResolver);
    }
    
    private void Configure<TSource, TTarget>(
        Action<IAdapterBuilder<TSource, TTarget>> configure,
        out MapperConfigurations configurations,
        out ActivationResolver activationContext)
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
    
    [Fact]
    public void Constructor_ConfigureAndResolve_FromInnerProperties()
    {
        // Arrange
        Configure<SourceGamma, TargetGamma>(
            builder =>
            {
                builder.Constructor()
                    .Parameters(p =>
                    {
                        p.InnerProperties(s => s.SourceBeta).InnerProperties(s => s.SourceAlpha, i =>
                        {
                            i.Parameter(s => s.Name);
                            i.Parameter(s => s.Age);
                        });
                        p.Parameter(s => s.EntryDate);
                    });
            },
            out var configuration, out var activationContext);
        
        // Act
        var resolution = activationContext.CreateResolution(configuration);
        
        // Assert
        Assert.NotNull(resolution);
        Assert.True(resolution.Resolved);
        Assert.NotNull(resolution.ConstructorResolution);
        Assert.Equal(3, resolution.ConstructorResolution.ParameterResolution.Count());
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

#region AlphaBethaGamma

file sealed class SourceAlpha
{
    public string Name { get; set; }
    public int Age { get; set; }
}

file sealed class TargetAlpha
{
    public TargetAlpha(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public string Name { get; }
    public int Age { get; }
}

file sealed class SourceBeta
{
    public SourceAlpha SourceAlpha { get; set; }
}

file sealed class TargetBeta
{
    public TargetBeta(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public string Name { get; }
    public int Age { get; }
}


file sealed class SourceGamma
{
    public SourceBeta SourceBeta { get; set; }

    public DateTime EntryDate { get; set; }
}

file sealed class TargetGamma
{
    public TargetGamma(string name, int age, DateTime entryDate)
    {
        Name = name;
        Age = age;
        EntryDate = entryDate;
    }

    public string Name { get; }
    public int Age { get; }
    public DateTime EntryDate { get; }
}

#endregion