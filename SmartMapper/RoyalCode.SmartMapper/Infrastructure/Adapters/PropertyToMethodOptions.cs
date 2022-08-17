using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

public class PropertyToMethodOptions : InnerPropertiesOptionsBase
{
    public MethodInfo? Method { get; internal set; }
    
    public string? MethodName { get; internal set; }
}