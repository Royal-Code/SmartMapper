using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

public record SourceProperty(PropertyInfo PropertyInfo,
    bool PreConfigured,
    PropertyOptions Options)
{
    private SourcePropertyResolution Resolution { get; } = new();

    public bool Resolved => Resolution.Resolved;
    
    // preciso de outra classe que trate hierarqui, pais e filhos.
}
