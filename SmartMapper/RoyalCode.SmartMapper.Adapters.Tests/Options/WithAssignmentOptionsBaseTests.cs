using System.Reflection;
using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Core;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Options;

public class WithAssignmentOptionsBaseTests
{
    private static readonly PropertyInfo ValuePropertyInfo = typeof(TestOptions).GetProperty("Value")!;
    
    [Fact]
    public void Reset_Must_Assign_RelatedProperty_ToNull()
    {
        var testOptions = new TestOptions();
        var propertyOptions = new PropertyOptions(ValuePropertyInfo);
        testOptions.PropertyRelated = propertyOptions;
        
        testOptions.Reset();

        testOptions.PropertyRelated.Should().BeNull();
    }

    [Fact]
    public void Reset_Must_ResetMappings_Of_RelatedProperty()
    {
        var testOptions = new TestOptions();
        var propertyOptions = new PropertyOptions(ValuePropertyInfo);
        propertyOptions.IgnoreMapping();
        testOptions.PropertyRelated = propertyOptions;
        
        testOptions.Reset();

        propertyOptions.ResolutionStatus.Should().Be(ResolutionStatus.Undefined);
    }
    
    private class TestOptions : WithAssignmentOptionsBase
    {
        public string Value { get; set; }
    }
}