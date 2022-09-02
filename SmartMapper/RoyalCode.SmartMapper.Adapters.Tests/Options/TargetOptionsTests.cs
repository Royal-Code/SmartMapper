using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Options;

public class TargetOptionsTests
{
    [Fact]
    public void GetConstructorOptions_Must_Return_SameInstances()
    {
        var options = new TargetOptions(typeof(Bar));
        
        var result1 = options.GetConstructorOptions();
        var result2 = options.GetConstructorOptions();
        
        result1.Should().NotBeNull().And.BeSameAs(result2);
    }
    
    private class Bar { }
}