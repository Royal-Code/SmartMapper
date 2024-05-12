using System.Linq.Expressions;
using RoyalCode.SmartMapper.Core.Discovery.Assignment;
using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Tests.Mappings.Discovery;

public class AssignmentDiscoveryTests
{
    [Fact]
    public void Direct_Strings()
    {
        // Arrange
        Util.PrepareConfiguration(out var configurations);
        var request = new AssignmentDiscoveryRequest(configurations, typeof(string), typeof(string));

        // Act
        var result = configurations.Discovery.Assignment.Discover(request);
        
        // Assert
        Assert.True(result.IsResolved);
        Assert.Equal(ValueAssignmentResolution.Direct, result.Resolution.Resolution);
    }

    [Fact]
    public void Direct_Interface_Impl()
    {
        // Arrange
        Util.PrepareConfiguration(out var configurations);
        var request = new AssignmentDiscoveryRequest(configurations, typeof(SourceImpl), typeof(ITarget));

        // Act
        var result = configurations.Discovery.Assignment.Discover(request);
        
        // Assert
        Assert.True(result.IsResolved);
        Assert.Equal(ValueAssignmentResolution.Direct, result.Resolution.Resolution);
    }

    [Fact]
    public void Direct_Int()
    {
        // Arrange
        Util.PrepareConfiguration(out var configurations);
        var request = new AssignmentDiscoveryRequest(configurations, typeof(int), typeof(int));

        // Act
        var result = configurations.Discovery.Assignment.Discover(request);
        
        // Assert
        Assert.True(result.IsResolved);
        Assert.Equal(ValueAssignmentResolution.Direct, result.Resolution.Resolution);
    }
    
    [Fact]
    public void Direct_Int_To_Nullable()
    {
        // Arrange
        Util.PrepareConfiguration(out var configurations);
        var request = new AssignmentDiscoveryRequest(configurations, typeof(int), typeof(int?));

        // Act
        var result = configurations.Discovery.Assignment.Discover(request);
        
        // Assert
        Assert.True(result.IsResolved);
        Assert.Equal(ValueAssignmentResolution.Direct, result.Resolution.Resolution);
    }
    
    [Fact]
    public void Direct_Nullable_To_Nullable()
    {
        // Arrange
        Util.PrepareConfiguration(out var configurations);
        var request = new AssignmentDiscoveryRequest(configurations, typeof(int?), typeof(int?));

        // Act
        var result = configurations.Discovery.Assignment.Discover(request);
        
        // Assert
        Assert.True(result.IsResolved);
        Assert.Equal(ValueAssignmentResolution.Direct, result.Resolution.Resolution);
    }
    
    [Fact]
    public void Direct_Nullable_To_Int()
    {
        // Arrange
        Util.PrepareConfiguration(out var configurations);
        var request = new AssignmentDiscoveryRequest(configurations, typeof(int?), typeof(int));

        // Act
        var result = configurations.Discovery.Assignment.Discover(request);
        
        // Assert
        Assert.True(result.IsResolved);
        Assert.Equal(ValueAssignmentResolution.Convert, result.Resolution.Resolution);
        Assert.NotNull(result.Resolution.Converter);
        
        var converter = result.Resolution.Converter;
        var expression = Assert.IsAssignableFrom<Expression<Func<int?, int>>>(converter.Converter);
        var func = expression.Compile();
        Assert.Equal(1, func(1));
        Assert.Equal(0, func(null));
    }

    [Fact]
    public void Direct_Enum()
    {
        // Arrange
        Util.PrepareConfiguration(out var configurations);
        var request = new AssignmentDiscoveryRequest(configurations, typeof(EnumSource), typeof(EnumSource));

        // Act
        var result = configurations.Discovery.Assignment.Discover(request);
        
        // Assert
        Assert.True(result.IsResolved);
        Assert.Equal(ValueAssignmentResolution.Direct, result.Resolution.Resolution);
    }

    [Fact]
    public void Cast_Enum()
    {
        // Arrange
        Util.PrepareConfiguration(out var configurations);
        var request = new AssignmentDiscoveryRequest(configurations, typeof(EnumSource), typeof(EnumTarget));
        
        // Act
        var result = configurations.Discovery.Assignment.Discover(request);
        
        // Assert
        Assert.True(result.IsResolved);
        Assert.Equal(ValueAssignmentResolution.Cast, result.Resolution.Resolution);
    }
    
    [Fact]
    public void Cast_Enum_Another_ExtraValue()
    {
        // arrange
        Util.PrepareConfiguration(out var configurations);
        var request = new AssignmentDiscoveryRequest(configurations, typeof(EnumSource), typeof(EnumAnother1));
        
        // act
        var result = configurations.Discovery.Assignment.Discover(request);
        
        // assert
        Assert.True(result.IsResolved);
        Assert.Equal(ValueAssignmentResolution.Cast, result.Resolution.Resolution);
    }
    
    [Fact]
    public void Cast_Enum_Another_LessValue()
    {
        // arrange
        Util.PrepareConfiguration(out var configurations);
        var request = new AssignmentDiscoveryRequest(configurations, typeof(EnumSource), typeof(EnumAnother2));
        
        // act
        var result = configurations.Discovery.Assignment.Discover(request);
        
        // assert
        Assert.False(result.IsResolved);
    }

    [Fact]
    public void Cast_Enum_Another_OtherNames()
    {
        // arrange
        Util.PrepareConfiguration(out var configurations);
        var request = new AssignmentDiscoveryRequest(configurations, typeof(EnumSource), typeof(EnumAnother3));
        
        // act
        var result = configurations.Discovery.Assignment.Discover(request);
        
        // assert
        Assert.False(result.IsResolved);
    }

    [Fact]
    public void Cast_Enum_NotSameType()
    {
        // arrange
        Util.PrepareConfiguration(out var configurations);
        var request = new AssignmentDiscoveryRequest(configurations, typeof(EnumSource), typeof(EnumLong));
        
        // act
        var result = configurations.Discovery.Assignment.Discover(request);
        
        // assert
        Assert.False(result.IsResolved);
    }
    
    [Fact]
    public void Cast_Int_To_Long()
    {
        // Arrange
        Util.PrepareConfiguration(out var configurations);
        var request = new AssignmentDiscoveryRequest(configurations, typeof(int), typeof(long));

        // Act
        var result = configurations.Discovery.Assignment.Discover(request);
        
        // Assert
        Assert.True(result.IsResolved);
        Assert.Equal(ValueAssignmentResolution.Cast, result.Resolution.Resolution);
    }

    [Fact]
    public void Cast_Long_To_Int()
    {
        // Arrange
        Util.PrepareConfiguration(out var configurations);
        var request = new AssignmentDiscoveryRequest(configurations, typeof(long), typeof(int));

        // Act
        var result = configurations.Discovery.Assignment.Discover(request);
        
        // Assert
        Assert.False(result.IsResolved);
    }
    
    [Fact]
    public void Cast_Bool_To_Int()
    {
        // Arrange
        Util.PrepareConfiguration(out var configurations);
        var request = new AssignmentDiscoveryRequest(configurations, typeof(bool), typeof(int));

        // Act
        var result = configurations.Discovery.Assignment.Discover(request);
        
        // Assert
        Assert.False(result.IsResolved);
    }
    
    [Fact]
    public void Cast_Bool_To_Byte()
    {
        // Arrange
        Util.PrepareConfiguration(out var configurations);
        var request = new AssignmentDiscoveryRequest(configurations, typeof(bool), typeof(byte));

        // Act
        var result = configurations.Discovery.Assignment.Discover(request);
        
        // Assert
        Assert.False(result.IsResolved);
    }
    
    [Fact]
    public void Cast_Float_To_Double()
    {
        // Arrange
        Util.PrepareConfiguration(out var configurations);
        var request = new AssignmentDiscoveryRequest(configurations, typeof(float), typeof(double));

        // Act
        var result = configurations.Discovery.Assignment.Discover(request);
        
        // Assert
        Assert.True(result.IsResolved);
        Assert.Equal(ValueAssignmentResolution.Cast, result.Resolution.Resolution);
    }

    [Fact]
    public void Cast_Double_To_Decimal()
    {
        // Arrange
        Util.PrepareConfiguration(out var configurations);
        var request = new AssignmentDiscoveryRequest(configurations, typeof(double), typeof(decimal));
        
        // Act
        var result = configurations.Discovery.Assignment.Discover(request);
        
        // Assert
        Assert.True(result.IsResolved);
        Assert.Equal(ValueAssignmentResolution.Cast, result.Resolution.Resolution);
    }
    
    [Fact]
    public void Cast_Long_To_Double()
    {
        // Arrange
        Util.PrepareConfiguration(out var configurations);
        var request = new AssignmentDiscoveryRequest(configurations, typeof(long), typeof(double));
        
        // Act
        var result = configurations.Discovery.Assignment.Discover(request);
        
        // Assert
        Assert.False(result.IsResolved);
    }
}

file class SourceImpl : ITarget { }
file interface ITarget { }

file enum EnumSource
{
    Value1,
    Value2
}

file enum EnumTarget
{
    Value1,
    Value2
}

file enum EnumAnother1
{
    Value1,
    Value2,
    Value3
}

file enum EnumAnother2
{
    Value1
}

file enum EnumAnother3
{
    Alpha,
    Beta
}

file enum EnumLong : byte
{
    Value1 = 1,
    Value2 = 2
}