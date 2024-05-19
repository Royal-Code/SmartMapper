
namespace RoyalCode.SmartMapper.Tests.Mappings.ToMethods;

public class MethodOptionsTests
{
    [Fact]
    public void MethodOptions_MustCreateTwoInstances_WhenMethodNotInformed()
    {
        // prepare
        Util.PrepareAdapter<Source, Target>(
            builder =>
            {
                // act
                builder.MapToMethod();
                builder.MapToMethod();
            },
            out _, out var activationContext);

        // assert
        var options = activationContext.Options;
        bool found = options.SourceOptions.TryGetSourceToMethodOptions(out var sourceToMethodOptions);
        Assert.True(found);
        Assert.NotNull(sourceToMethodOptions);
        Assert.Equal(2, sourceToMethodOptions.Count());

        var first = sourceToMethodOptions.First().MethodOptions;
        var second = sourceToMethodOptions.Last().MethodOptions;
        
        Assert.NotSame(first, second);
    }

    [Fact]
    public void MethodOptions_MustCreateOneInstance_WhenMethodDelegateInformed()
    {
        // prepare
        Util.PrepareAdapter<Source, Target>(
            builder =>
            {
                // act
                builder.MapToMethod(t => t.SetName);
                builder.MapToMethod(t => t.SetName);
            },
            out _, out var activationContext);
        
        // assert
        var options = activationContext.Options;
        bool found = options.SourceOptions.TryGetSourceToMethodOptions(out var sourceToMethodOptions);
        Assert.True(found);
        Assert.NotNull(sourceToMethodOptions);
        Assert.Single(sourceToMethodOptions);
        
        var first = sourceToMethodOptions.First().MethodOptions;
        var second = sourceToMethodOptions.Last().MethodOptions;
        
        Assert.Same(first, second);
    }

    [Fact]
    public void MethodOptions_MustCreateOneInstance_WhenMethodNameInformed()
    {
        // prepare
        Util.PrepareAdapter<Source, Target>(
            builder =>
            {
                // act
                builder.MapToMethod(nameof(Target.SetName));
                builder.MapToMethod(nameof(Target.SetName));
            },
            out _, out var activationContext);
        
        // assert
        var options = activationContext.Options;
        bool found = options.SourceOptions.TryGetSourceToMethodOptions(out var sourceToMethodOptions);
        Assert.True(found);
        Assert.NotNull(sourceToMethodOptions);
        Assert.Single(sourceToMethodOptions);
        
        var first = sourceToMethodOptions.First().MethodOptions;
        var second = sourceToMethodOptions.Last().MethodOptions;
        
        Assert.Same(first, second);
    }

    [Fact]
    public void MethodOptions_MustCreateOneInstance_WhenMethodNameAndDelegateInformed()
    {
        // prepare
        Util.PrepareAdapter<Source, Target>(
            builder =>
            {
                // act
                builder.MapToMethod(nameof(Target.SetName));
                builder.MapToMethod(t => t.SetName);
            },
            out _, out var activationContext);
        
        // assert
        var options = activationContext.Options;
        bool found = options.SourceOptions.TryGetSourceToMethodOptions(out var sourceToMethodOptions);
        Assert.True(found);
        Assert.NotNull(sourceToMethodOptions);
        Assert.Single(sourceToMethodOptions);
        
        var first = sourceToMethodOptions.First().MethodOptions;
        var second = sourceToMethodOptions.Last().MethodOptions;
        
        Assert.Same(first, second);
    }
}

file class Source
{
    public string Name { get; set; }
    
    public int Age { get; set; }
}

file class Target
{
    public void SetName(string name) => throw new NotImplementedException();
    
    public void SetAge(int age) => throw new NotImplementedException();
}