using RoyalCode.SmartMapper.Mapping.Builders.Internal;
using RoyalCode.SmartMapper.Mapping.Options;

namespace RoyalCode.SmartMapper.Examples;

internal sealed class AdapterConstrutorMappgins
{
    public void MapConstructorParameters()
    {
        var adapter = MappingOptions.AdapterFor<SourceAlpha, TargetAlpha>();
        var builder = new AdapterBuilder<SourceAlpha, TargetAlpha>(adapter);

        builder.Constructor()
            .Parameters(p =>
            {
                p.Parameter(s => s.Name);
                p.Parameter(s => s.Age);
            });
    }

    public void MapPropertyToConstructor()
    {
        var adapter = MappingOptions.AdapterFor<SourceBeta, TargetBeta>();
        var builder = new AdapterBuilder<SourceBeta, TargetBeta>(adapter);

        builder.Constructor().Map(s => s.SourceAlpha);
    }

    public void MapConstructorParametersFromInnerProperties()
    {
        var adapter = MappingOptions.AdapterFor<SourceGamma, TargetGamma>();
        var builder = new AdapterBuilder<SourceGamma, TargetGamma>(adapter);

        builder.Constructor()
            .Parameters(p =>
            {
                p.InnerProperties(s => s.SourceBeta).InnerProperties(s => s.SourceAlpha, i =>
                {
                    i.Parameter(s => s.Name);
                    i.Parameter(s => s.Age);
                });
                p.Parameter(s => s.EntryDate);
            });
    }
}

file sealed class SourceAlpha
{
    public string Name { get; set; }
    public int Age { get; set; }
}

file sealed class TargetAlpha
{
    public TargetAlpha(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public string Name { get; }
    public int Age { get; }
}

file sealed class SourceBeta
{
    public SourceAlpha SourceAlpha { get; set; }
}

file sealed class TargetBeta
{
    public TargetBeta(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public string Name { get; }
    public int Age { get; }
}


file sealed class SourceGamma
{
    public SourceBeta SourceBeta { get; set; }

    public DateTime EntryDate { get; set; }
}

file sealed class TargetGamma
{
    public TargetGamma(string name, int age, DateTime entryDate)
    {
        Name = name;
        Age = age;
        EntryDate = entryDate;
    }

    public string Name { get; }
    public int Age { get; }
    public DateTime EntryDate { get; }
}