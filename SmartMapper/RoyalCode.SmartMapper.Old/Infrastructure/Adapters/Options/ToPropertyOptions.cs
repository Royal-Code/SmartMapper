using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

public class ToPropertyOptions : ResolutionOptions
{
    public ToPropertyOptions(Type targetType, PropertyInfo targetProperty)
    {
        TargetType = targetType;
        TargetProperty = targetProperty;
    }
    
    public Type TargetType { get; }
    
    public PropertyInfo TargetProperty { get; }

    public ThenToOptionsBase? ThenTo { get; private set; }

    public ThenToPropertyOptions ThenToProperty(PropertyInfo propertyInfo)
    {
        if (ResolvedProperty is null)
            throw new InvalidOperationException("The 'ResolvedProperty' is not assigned");
        
        var targetProperty = new ToPropertyOptions(TargetProperty.PropertyType, propertyInfo);
        var thenToProperty = new ThenToPropertyOptions(this, targetProperty);
        
        ResolvedProperty.ThenMappedTo(thenToProperty);

        ThenTo = thenToProperty;
        return thenToProperty;
    }

    public ThenToMethodOptions ThenToMethod(MethodOptions methodOptions)
    {
        if (ResolvedProperty is null)
            throw new InvalidOperationException("The 'ResolvedProperty' is not assigned");

        var targetMethod = new ToMethodOptions(ResolvedProperty, methodOptions);
        var thenToMethod = new ThenToMethodOptions(this, targetMethod);
        
        ResolvedProperty.ThenMappedTo(thenToMethod);

        ThenTo = thenToMethod;
        return thenToMethod;
    }
}