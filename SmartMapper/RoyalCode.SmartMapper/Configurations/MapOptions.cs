using System.Reflection;

namespace RoyalCode.SmartMapper.Configurations;

public class MapOptions
{
    private Dictionary<PropertyInfo, PropertyOptions>? propertyOptions;

    public PropertyOptions? GetPropertyOptions(PropertyInfo property)
    {
        return propertyOptions is null
            ? null
            : propertyOptions.ContainsKey(property)
                ? propertyOptions[property]
                : null;
    }
}

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