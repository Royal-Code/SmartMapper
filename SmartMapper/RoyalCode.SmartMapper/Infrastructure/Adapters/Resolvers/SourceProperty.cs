using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

public class SourceProperty
{
    public SourceProperty(
        PropertyInfo propertyInfo,
        bool preConfigured,
        PropertyOptions options)
    {
        Hierarchy = new(this);
        PropertyInfo = propertyInfo;
        PreConfigured = preConfigured;
        Options = options;
    }
    
    private SourcePropertyResolution Resolution { get; } = new();

    public bool Resolved => Resolution.Resolved;

    public SourcePropertyHierarchy Hierarchy { get; }
    
    public PropertyInfo PropertyInfo { get; }
    
    public bool PreConfigured { get; }
    
    public PropertyOptions Options { get; }

    public string GetPropertyPathString() => Hierarchy.GetPropertyPathString();
}
