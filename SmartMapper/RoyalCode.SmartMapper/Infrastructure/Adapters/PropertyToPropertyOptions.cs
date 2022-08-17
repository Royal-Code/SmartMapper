using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

public class PropertyToPropertyOptions : ToPropertyTargetRelatedOptionsBase
{
    public PropertyToPropertyOptions(Type targetType, PropertyInfo targetProperty)
    {
        TargetType = targetType;
        TargetProperty = targetProperty;
    }
    
    public Type TargetType { get; }
    
    public PropertyInfo TargetProperty { get; }
}