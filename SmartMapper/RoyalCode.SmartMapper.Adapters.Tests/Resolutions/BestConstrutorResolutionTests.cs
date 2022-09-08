using FluentAssertions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;
using Xunit;

namespace RoyalCode.SmartMapper.Adapters.Tests.Resolutions;

public class BestConstrutorResolutionTests
{
    [Fact]
    public void GetElegibleConstructors_Must_FindDefaultConstructor()
    {
        // arrange
        var constructorOptions = new ConstructorOptions(typeof(Foo));
        var resolution = new BestConstrutorResolution(constructorOptions);

        // act
        var constructors = resolution.GetElegibleConstructors();

        // assert
        constructors.Should().HaveCount(1);
    }
    
    private class Foo
    {
        public string Value { get; set; }
    }
}