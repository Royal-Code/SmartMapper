using System.Reflection;
using RoyalCode.SmartMapper.Adapters.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Options.Resolutions;

/// <summary>
/// The options to resolve a source property to a target property.
/// </summary>
public sealed class ToPropertyResolutionOptions : ResolutionOptionsBase
{
    /// <summary>
    /// Creates a new instance of <see cref="ToPropertyResolutionOptions"/>.
    /// </summary>
    /// <param name="resolvedProperty">The resolved property options.</param>
    /// <param name="targetProperty">The target property.</param>
    public ToPropertyResolutionOptions(PropertyOptions resolvedProperty, PropertyInfo targetProperty)
        : base(resolvedProperty)
    {
        Status = ResolutionStatus.MappedToProperty;
        TargetProperty = targetProperty;
    }
    
    /// <summary>
    /// The target property.
    /// </summary>
    public PropertyInfo TargetProperty { get; }
    
    /// <summary>
    /// The inner target property mapping.
    /// </summary>
    public ThenToPropertyOptions? ThenToProperty { get; private set; }
    
    /// <summary>
    /// Continues the mapping of the source property to an internal property of the target property.
    /// </summary>
    /// <param name="targetProperty">The internal property.</param>
    /// <returns>The options to resolve a source property to a target property.</returns>
    public ThenToPropertyOptions ThenTo(PropertyInfo targetProperty)
    {
        ThenToProperty = new(this, targetProperty);
        return ThenToProperty;
    }
}

