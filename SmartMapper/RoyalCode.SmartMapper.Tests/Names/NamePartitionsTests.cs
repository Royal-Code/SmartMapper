using RoyalCode.SmartMapper.Mapping.Discovery.Members;

namespace RoyalCode.SmartMapper.Tests.Names;

public class NamePartitionsTests
{
    [Fact]
    public void SimpleName_Must_Not_HaveParts()
    {
        // Arrange - Act
        var names = new NamePartitions("Simple");

        // Assert 
        Assert.False(names.HasParts);
    }

    [Theory]
    [InlineData("PascalCase", 2)]
    [InlineData("camelCase", 2)]
    [InlineData("MoreComplexName", 3)]
    [InlineData("ABCase", 2)]
    public void PascalName_Must_HaveParts(string name, int expected)
    {
        // Arrange - Act
        var names = new NamePartitions(name);

        // Assert
        Assert.True(names.HasParts);
        Assert.Equal(expected, names.Parts.Length);
    }

    [Fact]
    public void GetNameByIndex_Must_ReturnFullName_When_IndexIsZero()
    {
        // Arrange
        var name = "MyPascalCaseName";
        var names = new NamePartitions(name);

        // Act
        var wasAbleToGet = names.GetName(0, out var getted);

        // Assert
        Assert.True(wasAbleToGet);
        Assert.Equal(name, getted);
    }

    [Theory]
    [InlineData("MyProperty", 1, "Property")]
    [InlineData("MyPascalCaseName", 1, "PascalCaseName")]
    [InlineData("MyPascalCaseName", 2, "CaseName")]
    [InlineData("MyPascalCaseName", 3, "Name")]
    [InlineData("ABCase", 1, "Case")]
    public void GetNameByIndex_Must_ReturnPartsOfName(string name, int index, string expected)
    {
        // Arrange
        var names = new NamePartitions(name);

        // Act
        var wasAbleToGet = names.GetName(index, out var getted);

        // Assert
        Assert.True(wasAbleToGet);
        Assert.Equal(expected, getted);
    }

    [Fact]
    public void GetNameByIndexAndEnd_Must_ReturnFullName_When_IndexAndEndIsZero()
    {
        // Arrange
        var name = "MyPascalCaseName";
        var names = new NamePartitions(name);

        // Act
        var wasAbleToGet = names.GetName(0, 0, out var getted);

        // Assert
        Assert.True(wasAbleToGet);
        Assert.Equal(name, getted);
    }

    [Theory]
    [InlineData("MyProperty", 1, 0, "Property")]
    [InlineData("MyPascalCaseName", 1, 0, "PascalCaseName")]
    [InlineData("MyPascalCaseName", 2, 0, "CaseName")]
    [InlineData("MyPascalCaseName", 3, 0, "Name")]
    [InlineData("ABCase", 1, 0, "Case")]
    [InlineData("ABCase", 0, 1, "AB")]
    [InlineData("MyProperty", 0, 1, "My")]
    [InlineData("MyPascalCaseName", 0, 1, "MyPascalCase")]
    [InlineData("MyPascalCaseName", 0, 2, "MyPascal")]
    [InlineData("MyPascalCaseName", 0, 3, "My")]
    [InlineData("MyPascalCaseName", 1, 1, "PascalCase")]
    [InlineData("MyPascalCaseName", 2, 1, "Case")]
    [InlineData("MyPascalCaseName", 1, 2, "Pascal")]
    public void GetNameByIndexAndEnd_Must_ReturnPartsOfName(string name, int index, int end, string expected)
    {
        // Arrange
        var names = new NamePartitions(name);

        // Act
        var wasAbleToGet = names.GetName(index, end, out var getted);

        // Assert
        Assert.True(wasAbleToGet);
        Assert.Equal(expected, getted);
    }

    [Fact]
    public void GetNameByIndex_Must_Fail_When_IndexOfRange()
    {
        // Arrange
        var name = "MyPascalCaseName";
        var names = new NamePartitions(name);

        // Act
        var wasAbleToGet = names.GetName(4, out var getted);

        // Assert
        Assert.False(wasAbleToGet);
        Assert.Null(getted);
    }

    [Theory]
    [InlineData("MyPascalCaseName", 4, 0)]
    [InlineData("MyPascalCaseName", 3, 1)]
    [InlineData("MyPascalCaseName", 2, 2)]
    [InlineData("MyPascalCaseName", 1, 3)]
    [InlineData("MyPascalCaseName", 0, 4)]
    [InlineData("MyPascalCaseName", 5, 0)]
    [InlineData("MyPascalCaseName", 0, 5)]
    [InlineData("MyPascalCaseName", 5, 5)]
    public void GetNameByIndex_Must__Fail_When_IndexOfRange(string name, int index, int end)
    {
        // Arrange
        var names = new NamePartitions(name);

        // Act
        var wasAbleToGet = names.GetName(index, end, out var getted);

        // Assert
        Assert.False(wasAbleToGet);
        Assert.Null(getted);
    }
}
