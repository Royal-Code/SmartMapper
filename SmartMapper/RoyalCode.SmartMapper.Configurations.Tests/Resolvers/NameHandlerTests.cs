using System.Linq;
using RoyalCode.SmartMapper.Infrastructure.Naming;
using Xunit;

namespace RoyalCode.SmartMapper.Configurations.Tests.Resolvers;

public class NameHandlerTests
{
    [Fact]
    public void MustRemovePrefixOfName()
    {
        var handler = new NameHandlerForTest("Key", null);
        
        var found = handler.GetNames("KeyTest").ToList();
        Assert.Single(found);
        
        Assert.Equal("Test", found.First());
    }
    
    [Fact]
    public void MustRemoveSuffixOfName()
    {
        var handler = new NameHandlerForTest(null, "Key");
        
        var found = handler.GetNames("TestKey").ToList();
        Assert.Single(found);
        
        Assert.Equal("Test", found.First());
    }
    
    [Fact]
    public void MustRemovePrefixAndSuffixOfName()
    {
        var handler = new NameHandlerForTest("key", "Key");
        
        var found = handler.GetNames("KeyTestKey").ToList();
        Assert.Equal(3, found.Count);
        
        Assert.Equal("Test", found.Last());
    }
    
    [Theory]
    [InlineData("Key", "KeyValue", true)]
    [InlineData("key", "KeyValue", true)]
    [InlineData("KEY", "keyvalue", true)]
    [InlineData("Id", "EntityId", false)]
    public void SimplePrefixTests(string prefix, string name, bool match)
    {
        var handler = new NameHandlerForTest(prefix, null);

        var found = handler.GetNames(name).Any();

        Assert.Equal(match, found);
    }
    
    [Theory]
    [InlineData("Key", "KeyValue", false)]
    [InlineData("Id", "EntityId", true)]
    [InlineData("id", "EntityId", true)]
    [InlineData("ID", "entityid", true)]
    public void SimpleSuffixTests(string suffix, string name, bool match)
    {
        var handler = new NameHandlerForTest(null, suffix);

        var found = handler.GetNames(name).Any();

        Assert.Equal(match, found);
    }
    
    [Theory]
    [InlineData("Key", "Value", "KeyValue", 3)]
    [InlineData("Id", "Value", "KeyValue", 1)]
    [InlineData("Id", "Value", "EntityId", 0)]
    [InlineData("Key", "Id", "EntityId", 1)]
    [InlineData("Key", "Id", "KeyValue", 1)]
    public void PrefixAndSuffixTests(string prefix, string suffix, string name, int count)
    {
        var handler = new NameHandlerForTest(prefix, suffix);

        var found = handler.GetNames(name).Count();

        Assert.Equal(count, found);
    }
    
    private class NameHandlerForTest : NameHandlerBase
    {
        public NameHandlerForTest(string? prefix, string? suffix)
        {
            Prefix = prefix;
            Suffix = suffix;
        }
        
        public override void Validate(NamingContext context) { }
    }
}