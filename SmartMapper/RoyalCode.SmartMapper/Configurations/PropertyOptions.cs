using System.Reflection;

namespace RoyalCode.SmartMapper.Configurations;

public class PropertyOptions
{
    private Dictionary<string, object>? annotations;

    public PropertyOptions(PropertyInfo sourceProperty)
    {
        SourceProperty = sourceProperty;
    }
    
    public PropertyInfo SourceProperty { get; }
    
    public PropertyInfo? TargetProperty { get; internal set; }
    
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
