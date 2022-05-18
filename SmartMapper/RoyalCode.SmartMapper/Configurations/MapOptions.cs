using System.Reflection;

namespace RoyalCode.SmartMapper.Configurations;

public interface IMapOptions
{
    public PropertyOptions? GetPropertyOptions(PropertyInfo property);

    public PropertyOptions GetOrCreatePropertyOptions(PropertyInfo property);
}

public class MapOptions
{
    private Dictionary<MapPropertyKey, PropertyOptions>? propertyOptions;

    // TODO: can polymorphism be used? and remove MapKind and the key?
    public PropertyOptions? GetAdapterPropertyOptions(PropertyInfo property)
    {
        var key = new MapPropertyKey(property, MapKind.Adapter);
        return propertyOptions is null
            ? null
            : propertyOptions.ContainsKey(key)
                ? propertyOptions[key]
                : null;
    }

    public PropertyOptions GetOrCreateAdapterPropertyOptions(PropertyInfo property)
    {
        var options = GetAdapterPropertyOptions(property);
        if (options is null)
        {
            options = new PropertyOptions(property);
            propertyOptions ??= new();
            propertyOptions.Add(new MapPropertyKey(property, MapKind.Adapter), options);
        }

        return options;
    }
}

public record MapPropertyKey(PropertyInfo Property, MapKind Kind);

public class MapOptions<TSource, TTarget> : MapOptions
{
    
}

public enum MapKind
{
    /// <summary>
    /// <para>
    ///     Used to create a new target object, 
    ///     where the values of the source object's properties are read and assigned to the target object's properties.
    /// </para>
    /// <para>
    ///     
    /// </para>
    /// </summary>
    Adapter,

    /// <summary>
    /// <para>
    ///     Used in mappings where a 'new' expression is generated for the target object and its properties initialized.
    /// </para>
    /// <para>
    ///     It can be used in the <c>Select</c> method of Linq namespace.
    /// </para>
    /// </summary>
    Selector,

    Specifier
}
