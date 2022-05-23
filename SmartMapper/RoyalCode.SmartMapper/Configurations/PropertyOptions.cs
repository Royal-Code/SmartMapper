using System.Reflection;
using RoyalCode.Extensions.PropertySelection;

namespace RoyalCode.SmartMapper.Configurations;

public class PropertyOptions
{
    private Dictionary<string, object>? annotations;

    public PropertyOptions(PropertyInfo sourceProperty)
    {
        SourceProperty = sourceProperty;
    }
    
    public PropertyInfo SourceProperty { get; }
    
    public PropertySelection? TargetProperty { get; internal set; }
    
    public PropertyToMethodOptions? TargetMethodOptions { get; internal set; }
    
    public PropertyMapAction Action { get; internal set; }

    public object? FindAnnotation(string alias)
    {
        if (annotations is null || !annotations.ContainsKey(alias))
            return null;
        return annotations[alias];
    }

    public T? FindAnnotation<T>(string alias)
    {
        var obj = FindAnnotation(alias);
        return obj is T value ? value : default;
    }

    public void SetAnnotation(string alias, object value)
    {
        annotations ??= new();
        annotations[alias] = value;
    }
}

public class PropertyToMethodOptions
{
    public PropertyToMethodOptions(PropertyOptions propertyOptions)
    {
        PropertyOptions = propertyOptions;
    }

    public PropertyOptions PropertyOptions { get; }

    public PropertyInfo SourceProperty => PropertyOptions.SourceProperty;
    
    public MethodInfo? TargetMethod { get; internal set; }
    
    public string MethodName { get; internal set; }
}

public class PropertyToParameterOptions
{
    public PropertyToParameterOptions(PropertyToMethodOptions propertyToMethodOptions)
    {
        PropertyToMethodOptions = propertyToMethodOptions;
    }

    public PropertyToMethodOptions PropertyToMethodOptions { get; }
    
    
}