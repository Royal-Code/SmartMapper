using System;
using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Core;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Options;

public class OptionsBaseTests
{
    [Fact]
    public void FindAnnotation_ByAlias_Must_ReturnNull_When_HasNonAnnotation()
    {
        var options = new TestOptions();
        var result = options.FindAnnotation("test");
        result.Should().BeNull();
    }
    
    [Fact]
    public void FindAnnotation_ByAlias_Must_ReturnNull_When_NotFound()
    {
        var options = new TestOptions();
        options.SetAnnotation("test", "test");
        var result = options.FindAnnotation("notfound");
        result.Should().BeNull();
    }
    
    [Fact]
    public void FindAnnotation_ByAlias_Must_ReturnValue_X1()
    {
        var options = new TestOptions();
        options.SetAnnotation("test", "test");
        var result = options.FindAnnotation("test");
        result.Should().Be("test");
    }

    [Fact]
    public void FindAnnotation_ByAlias_Must_ReturnValue_X3()
    {
        var options = new TestOptions();
        options.SetAnnotation("test", "test");
        options.SetAnnotation("test", "test2");
        options.SetAnnotation("test", "test3");
        var result = options.FindAnnotation("test");
        result.Should().Be("test3");
    }
    
    [Fact]
    public void FindAnnotation_ByAlias_Must_ReturnCorrectValue_In_Case_Of_Multiple_Annotations()
    {
        var options = new TestOptions();
        options.SetAnnotation("test", "test");
        options.SetAnnotation("test2", "test2");
        options.SetAnnotation("test3", "test3");
        options.SetAnnotation("test4", "test4");
        
        var result = options.FindAnnotation("test");
        result.Should().Be("test");
    }

    [Fact]
    public void FindAnnotation_ByType_Must_ReturnNull_When_HasNonAnnotation()
    {
        var options = new TestOptions();
        var result = options.FindAnnotation<string>();
        result.Should().BeNull();
    }
    
    [Fact]
    public void FindAnnotation_ByType_Must_ReturnNull_When_NotFound()
    {
        var options = new TestOptions();
        options.SetAnnotation<string>("test");
        var result = options.FindAnnotation<int?>();
        result.Should().BeNull();
    }
    
    [Fact]
    public void FindAnnotation_ByType_Must_ReturnValue_X1()
    {
        var options = new TestOptions();
        options.SetAnnotation<string>("test");
        var result = options.FindAnnotation<string>();
        result.Should().Be("test");
    }
    
    [Fact]
    public void FindAnnotation_ByType_Must_ReturnValue_X3()
    {
        var options = new TestOptions();
        options.SetAnnotation<string>("test");
        options.SetAnnotation<string>("test2");
        options.SetAnnotation<string>("test3");
        var result = options.FindAnnotation<string>();
        result.Should().Be("test3");
    }
    
    [Fact]
    public void FindAnnotation_ByType_Must_ReturnCorrectValue_In_Case_Of_Multiple_Annotations()
    {
        var options = new TestOptions();
        options.SetAnnotation<string>("test");
        options.SetAnnotation<int?>(2);
        options.SetAnnotation<long?>(3);
        options.SetAnnotation<byte?>(4);
        
        var result = options.FindAnnotation<string>();
        result.Should().Be("test");
    }
    
    private class TestOptions : OptionsBase { }
}