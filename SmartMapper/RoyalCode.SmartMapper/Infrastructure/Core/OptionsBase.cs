namespace RoyalCode.SmartMapper.Infrastructure.Core;

/// <summary>
/// <para>
///     Shared functionalities between options.
/// </para>
/// </summary>
public abstract class OptionsBase
{
    private Dictionary<string, object>? annotations;
    
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
    
    public T? FindAnnotation<T>()
    {
        var obj = FindAnnotation(typeof(T).Name);
        return obj is T value ? value : default;
    }
    
    public void SetAnnotation<T>(object value)
    {
        annotations ??= new();
        annotations[typeof(T).Name] = value;
    }
}