using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

public class PropertyToPropertyOptions : TargetRelatedOptionsBase
{
    private readonly ICollection<ThenToPropertyOptions>? thenToProperties;

    public PropertyToPropertyOptions(Type targetType, PropertyInfo targetProperty)
    {
        TargetType = targetType;
        TargetProperty = targetProperty;
    }
    
    public Type TargetType { get; }
    
    public PropertyInfo TargetProperty { get; }

    public ThenToPropertyOptions ThenTo(PropertyInfo propertyInfo)
    {
        
        
        throw new NotImplementedException();
    }
}