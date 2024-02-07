
namespace RoyalCode.SmartMapper.Tests.UseCases.Models;

#pragma warning disable

public class ComplexCtorDto
{
    public ComplexCtorValuesA ValuesA { get; set; }

    public ComplexCtorValuesB ValuesB { get; set; }

    public decimal Value0 { get; set; }
}

public class ComplexCtorValuesA
{
    public int Value1 { get; set; }

    public long Value2 { get; set; }
}

public class ComplexCtorValuesB
{
    public string Value3 { get; set; }

    public string Value4 { get; set; }
}
