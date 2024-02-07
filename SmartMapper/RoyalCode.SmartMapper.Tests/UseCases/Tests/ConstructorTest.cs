
using RoyalCode.SmartMapper.Tests.UseCases.Models;

namespace RoyalCode.SmartMapper.Adapters.Testings.UseCases.Tests;

public class ConstructorTest
{
    [Fact]
    public void Empty()
    {
        var sp = DIContainer.GetServiceProvider();
        var adapter = sp.GetRequiredService<IAdapter>();

        var dto = new EmptyCtorDto();
        var dm = adapter.Map<EmptyCtorDto, EmptyCtorDm>(dto);

        Assert.NotNull(dm);
        Assert.Null(dm.Value);
    }

    [Fact]
    public void Simple()
    {
        var injector = DIContainer.GetServiceProvider();
        var adapter = injector.GetRequiredService<IAdapter>();

        string value = "V2";

        var dto = new SimpleCtorDto() { Value2 = value };
        var dm = adapter.Map<SimpleCtorDto, SimpleCtorDm>(dto);

        Assert.NotNull(dm);
        Assert.Null(dm.Value1);
        Assert.Equal(value, dm.Value2);
    }

    [Fact]
    public void Base()
    {
        var injector = DIContainer.GetServiceProvider();
        var adapter = injector.GetRequiredService<IAdapter>();

        string value = "V3";

        var dto = new BaseCtorDto()
        {
            CtorValues = new BaseCtorValues()
            {
                Value1 = 1,
                Value2 = 2,
                Value3 = value
            }
        };
        var dm = adapter.Map<BaseCtorDto, BaseCtorDm>(dto);

        Assert.NotNull(dm);
        Assert.NotEqual(0, dm.Value1);
        Assert.NotEqual(0, dm.Value2);
        Assert.Equal(value, dm.Value3);
    }

    [Fact]
    public void Complex()
    {
        var injector = DIContainer.GetServiceProvider();
        var adapter = injector.GetRequiredService<IAdapter>();

        string value = "V4";
        string walue = "V5";
        decimal money = 99.90M;

        var dto = new ComplexCtorDto()
        {
            ValuesA = new ComplexCtorValuesA()
            {
                Value1 = 1,
                Value2 = 2,
            },
            ValuesB = new ComplexCtorValuesB()
            {
                Value3 = value,
                Value4 = walue,
            },
            Value0 = money
        };
        var dm = adapter.Map<ComplexCtorDto, ComplexCtorDm>(dto);

        Assert.NotNull(dm);
        Assert.Equal(1, dm.Value1);
        Assert.Equal(2, dm.Value2);
        Assert.Equal(value, dm.Value3);
        Assert.Equal(walue, dm.Value4);
        Assert.Equal(money, dm.Value0);
    }
}
