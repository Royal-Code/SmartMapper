using System.Reflection;

namespace RoyalCode.SmartMapper.Configurations;

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
}

public record MapPropertyKey(PropertyInfo Property, MapKind Kind);

public class MapOptions<TSource, TTarget> : MapOptions
{
    
}

public class PropertyOptions
{
    public PropertyOptions(PropertyInfo sourceProperty)
    {
        SourceProperty = sourceProperty;
    }
    
    public PropertyInfo SourceProperty { get; }

    public PropertyInfo? TargetProperty { get; set; }
    
    public PropertyMapAction Action { get; set; }
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

public enum PropertyMapAction
{
    /// <summary>
    /// Action not defined.
    /// </summary>
    Undefined,
    
    /// <summary>
    /// <para>
    ///     Set the value from source property to the target property.
    /// </para>
    /// <para>
    ///     Value converters can be used.
    /// </para>
    /// </summary>
    SetValue,
    
    /// <summary>
    /// <para>
    ///     Map the properties of the source property type to target properties.
    /// </para>
    /// </summary>
    MapProperties,
    
    /// <summary>
    /// <para>
    ///     Adapt the source property type to the target property type.
    /// </para>
    /// </summary>
    Adapt,
    
    /// <summary>
    /// <para>
    ///     Select (get) values from source properties and set to target type properties.
    /// </para>
    /// </summary>
    Select,
    
    /// <summary>
    /// <para>
    ///     Ignore the property.
    /// </para>
    /// </summary>
    Ignore,
    
    
}