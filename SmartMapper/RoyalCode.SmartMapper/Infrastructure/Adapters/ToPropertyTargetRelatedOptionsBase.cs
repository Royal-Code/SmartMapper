using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

public class ToPropertyTargetRelatedOptionsBase : TargetRelatedOptionsBase
{
    public ThenToKind ThenToKind { get; private set; } = ThenToKind.NotApplied;
    
    public ThenToPropertyOptions? ThenToPropertyOptions { get; private set; }
    
    public PropertyToMethodOptions? PropertyToMethodOptions { get; private set; }
    
    public ThenToPropertyOptions ThenTo(PropertyInfo propertyInfo)
    {
        ThenToKind = ThenToKind.Property;
        ThenToPropertyOptions = new ThenToPropertyOptions(this, propertyInfo);
        return ThenToPropertyOptions;
    }
    
    public PropertyToMethodOptions ThenToMethod(MethodInfo? methodInfo = null)
    {
        ThenToKind = ThenToKind.Method;
        PropertyToMethodOptions = new PropertyToMethodOptions();
        
        if (methodInfo is not null)
        {
            PropertyToMethodOptions.Method = methodInfo;
            PropertyToMethodOptions.MethodName = methodInfo.Name;
        }

        return PropertyToMethodOptions;
    }
}