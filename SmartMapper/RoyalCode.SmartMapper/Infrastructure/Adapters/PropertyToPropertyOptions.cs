using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

public class PropertyToPropertyOptions : TargetRelatedOptionsBase
{
    public PropertyToPropertyOptions(Type targetType, PropertyInfo targetProperty)
    {
        TargetType = targetType;
        TargetProperty = targetProperty;
    }
    
    public Type TargetType { get; }
    
    public PropertyInfo TargetProperty { get; }

    public ThenToPropertyOptions? ThenToPropertyOptions { get; private set; }
    
    public ThenToPropertyOptions ThenTo(PropertyInfo propertyInfo)
    {
        ThenToPropertyOptions = new ThenToPropertyOptions(this, propertyInfo);
        return ThenToPropertyOptions;
    }
}