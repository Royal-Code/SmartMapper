using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Core;

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

    public virtual bool Validate(PropertyInfo sourceProperty, Type targetType)
    {
        // it's not clair about what will be doned here.
        // how it will instruct about how the strategy must be used for set the value.
        
        // use cases:
        // on adapt, a property with Id, must use a EntityProviderService to load the entity.
        // on select, a property with First, must use the First method of an IEnumerable extension.

        return true;
    }
}

public class SourceNameHandler : NameHandlerBase
{
    
}

public class TargetNameHandler : NameHandlerBase
{
    
}