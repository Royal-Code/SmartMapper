using Xunit;

namespace RoyalCode.SmartMapper.Configurations.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        //var configuration = new MapperConfiguration();

        //var options = configuration.Configure<SimplePlainDto, SimplePlainEntity>();
    }
}

public class SimplePlainDto
{
    public int Id { get; set; }

    public string Name { get; set; }
}

public class SimplePlainEntity
{
    public int Id { get; set; }

    public string Name { get; set; }
}