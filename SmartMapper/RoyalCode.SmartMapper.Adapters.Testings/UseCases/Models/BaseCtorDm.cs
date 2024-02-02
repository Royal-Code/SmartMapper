
namespace RoyalCode.SmartMapper.Adapters.Testings.UseCases.Models;

public class BaseCtorDm
{
    public int Value1 { get; }

    public long Value2 { get; }

    public string Value3 { get; }

    public BaseCtorDm(int value1, long value2, string value3)
    {
        Value1 = value1;
        Value2 = value2;
        Value3 = value3;
    }
}
