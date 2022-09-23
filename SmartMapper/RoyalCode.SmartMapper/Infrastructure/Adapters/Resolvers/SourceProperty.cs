using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

public class SourceProperty
{
    public PropertyInfo PropertyInfo { get; init; }
    
    public bool PreConfigured { get; init; }

    public PropertyOptions Options { get; init; }
    
    public bool Resolved { get; internal set; }
}