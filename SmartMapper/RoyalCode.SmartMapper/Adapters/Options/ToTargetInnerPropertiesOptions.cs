using System.Reflection;

namespace RoyalCode.SmartMapper.Adapters.Options;

/// <summary>
/// Options for inner properties of a target property.
/// </summary>
public sealed class ToTargetInnerPropertiesOptions
{
    private ICollection<ToTargetPropertyOptions>? innerProperties;

    /// <summary>
    /// Creates a new instance of <see cref="ToTargetInnerPropertiesOptions"/>.
    /// </summary>
    /// <param name="parentTargetProperty">The parent target property options.</param>
    public ToTargetInnerPropertiesOptions(ToTargetPropertyOptions parentTargetProperty)
    {
        ParentTargetProperty = parentTargetProperty;
        PropertyTargetOptions = new TargetOptions(parentTargetProperty.TargetProperty.PropertyType);
    }
    
    /// <summary>
    /// The parent target property options.
    /// </summary>
    public ToTargetPropertyOptions ParentTargetProperty { get; }

    /// <summary>
    /// The target options for the target property.
    /// </summary>
    public TargetOptions PropertyTargetOptions { get; }
    
    /// <summary>
    /// The inner properties.
    /// </summary>
    public IEnumerable<ToTargetPropertyOptions> InnerProperties => innerProperties ?? Enumerable.Empty<ToTargetPropertyOptions>();
    
    /// <summary>
    /// Get or create the inner property options for the target property.
    /// </summary>
    /// <param name="innerTargetPropertyInfo"></param>
    /// <returns></returns>
    public ToTargetPropertyOptions GetInnerProperty(PropertyInfo innerTargetPropertyInfo)
    {
        var options = innerProperties?.FirstOrDefault(x => x.TargetProperty == innerTargetPropertyInfo);
        if (options is not null)
            return options;
        
        // validate if the property is a target property
        if (ParentTargetProperty.TargetProperty.PropertyType != innerTargetPropertyInfo.DeclaringType)
            throw new ArgumentException(
                $"The property '{innerTargetPropertyInfo.Name}' is not an inner property of the target property '{ParentTargetProperty.TargetProperty.Name}'.",
                nameof(innerTargetPropertyInfo));
        
        // create a new options and add to the inner properties collection
        options = PropertyTargetOptions.GetOrCreateToTargetPropertyOptions(
            ParentTargetProperty.SourcePropertyOptions,
            innerTargetPropertyInfo);
        
        innerProperties ??= [];
        innerProperties.Add(options);
        return options;
    }
}