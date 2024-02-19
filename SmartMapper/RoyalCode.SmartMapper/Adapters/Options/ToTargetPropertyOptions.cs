using System.Reflection;

namespace RoyalCode.SmartMapper.Adapters.Options;

/// <summary>
/// Options of a target property mapped from a source property.
/// </summary>
public sealed class ToTargetPropertyOptions
{
    /// <summary>
    /// Creates a new instance of <see cref="ToTargetPropertyOptions"/>.
    /// </summary>
    /// <param name="targetOptions">The target type options.</param>
    /// <param name="targetProperty">The target property.</param>
    /// <param name="sourcePropertyOptions">The source property options.</param>
    public ToTargetPropertyOptions(
        TargetOptions targetOptions,
        PropertyInfo targetProperty,
        PropertyOptions sourcePropertyOptions)
    {
        TargetOptions = targetOptions;
        TargetProperty = targetProperty;
        SourcePropertyOptions = sourcePropertyOptions;
    }
    
    /// <summary>
    /// The target type options.
    /// </summary>
    public TargetOptions TargetOptions { get; }
    
    /// <summary>
    /// The target property.
    /// </summary>
    public PropertyInfo TargetProperty { get; }
    
    /// <summary>
    /// The source property options.
    /// </summary>
    public PropertyOptions SourcePropertyOptions { get; }
    
    /// <summary>
    /// Options for inner properties of the target property.
    /// </summary>
    public ToTargetInnerPropertiesOptions? InnerPropertiesOptions { get; private set; }

    /// <summary>
    /// Get a inner property options.
    /// </summary>
    /// <param name="innerProperty">The property info of the inner property.</param>
    /// <returns>
    ///     A instance of <see cref="ToTargetPropertyOptions"/>.
    /// </returns>
    public ToTargetPropertyOptions GetInnerProperty(PropertyInfo innerProperty)
    {
        InnerPropertiesOptions ??= new(this);
        return InnerPropertiesOptions.GetInnerProperty(innerProperty);
    }
}