using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

public class PropertyToMethodOptions : ToParametersTargetRelatedOptionsBase
{
    public PropertyToMethodOptions(Type propertyType)
        : base(propertyType)
    { }

    public MethodInfo? Method { get; internal set; }
    
    public string? MethodName { get; internal set; }
}