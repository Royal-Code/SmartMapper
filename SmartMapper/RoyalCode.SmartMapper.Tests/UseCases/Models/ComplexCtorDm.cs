
namespace RoyalCode.SmartMapper.Tests.UseCases.Models;

public class ComplexCtorDm
{
    public decimal Value0 { get; }

    public int Value1 { get; }

    public long Value2 { get; }

    public string Value3 { get; }

    public string Value4 { get; }

    public ComplexCtorDm(decimal value0, int value1, long value2, string value3, string value4)
    {
        Value0 = value0;
        Value1 = value1;
        Value2 = value2;
        Value3 = value3;
        Value4 = value4;
    }
}
