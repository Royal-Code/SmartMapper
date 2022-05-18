namespace RoyalCode.SmartMapper.Configurations;

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