using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

public class ThenToPropertyOptions : TargetRelatedOptionsBase
{
    public ThenToPropertyOptions(TargetRelatedOptionsBase parentOptions, PropertyInfo propertyInfo)
    {
        ParentOptions = parentOptions;
        PropertyInfo = propertyInfo;
    }
    
    public TargetRelatedOptionsBase ParentOptions { get; }
    
    public PropertyInfo PropertyInfo { get; }
}