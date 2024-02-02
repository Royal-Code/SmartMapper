using System.Diagnostics.CodeAnalysis;

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
        return FindAnnotation<T>(typeof(T).Name);
    }
    
    public void SetAnnotation<T>(object value)
    {
        SetAnnotation(typeof(T).Name, value);
    }
    
    public bool TryFindAnnotation<T>([NotNullWhen(true)] out T? annotation)
    {
        annotation = FindAnnotation<T>(typeof(T).Name);
        return annotation is not null;
    }
}