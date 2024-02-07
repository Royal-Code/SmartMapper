
namespace RoyalCode.SmartMapper.Tests.UseCases.Models;

#pragma warning disable

public class SimpleCtorDm
{
    public string Value1 { get; set; }

    public string Value2 { get; }

    public SimpleCtorDm(string value2)
    {
        Value2 = value2;
    }
}
