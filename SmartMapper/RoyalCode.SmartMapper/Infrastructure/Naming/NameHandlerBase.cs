
namespace RoyalCode.SmartMapper.Infrastructure.Naming;

public abstract class NameHandlerBase
{
    public string? Suffix { get; protected set; }

    public string? Prefix { get; protected set; }
    
    public IEnumerable<string> GetNames(string name)
    {
        bool hasPrefix = false;
        bool hasSuffix = false;

        if (Prefix is not null && name.StartsWith(Prefix, StringComparison.OrdinalIgnoreCase))
        {
            hasPrefix = true;
            yield return name[Prefix.Length..];
        }

        if (Suffix is not null && name.EndsWith(Suffix, StringComparison.OrdinalIgnoreCase))
        {
            hasSuffix = true;
            yield return name[..^Suffix.Length];
        }

        if (hasPrefix && hasSuffix)
            yield return name[Prefix!.Length..^Suffix!.Length];
    }

    public abstract void Validate(NamingContext context);
    // {
    //     // it's not clair about what will be doned here.
    //     // how it will instruct about how the strategy must be used for set the value.
    //
    //     // use cases:
    //     // on adapt, a property with Id, must use a EntityProviderService to load the entity.
    //     // on select, a property with First, must use the First method of an IEnumerable extension.
    //
    //     resolution = new();
    //     return true;
    // }
}