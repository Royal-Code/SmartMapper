namespace RoyalCode.SmartMapper.Resolvers;

public class NameHandlerBase
{
    public string? Suffix { get; set; }

    public string? Prefix { get; set; }
    
    public IEnumerable<string> GetNames(string name)
    {
        bool p = false;
        bool s = false;

        if (Prefix is not null && name.StartsWith(Prefix, StringComparison.OrdinalIgnoreCase))
        {
            p = true;
            yield return name[Prefix.Length..];
        }

        if (Suffix is not null && name.EndsWith(Suffix, StringComparison.OrdinalIgnoreCase))
        {
            s = true;
            yield return name[..^Suffix.Length];
        }

        if (p && s)
            yield return name[Prefix!.Length..^Suffix!.Length];
    }
}