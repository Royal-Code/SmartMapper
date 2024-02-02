
namespace RoyalCode.SmartMapper.Adapters.Testings.UseCases.Models;

#pragma warning disable

public class EmptyCtorDm
{
    public string Value { get; set; }

    public EmptyCtorDm() { }

    public EmptyCtorDm(string value)
    {
        Value = value;
    }
}
