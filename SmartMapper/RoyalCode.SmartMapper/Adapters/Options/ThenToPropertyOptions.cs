using System.Reflection;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Options;

/// <summary>
/// Options to resolve a source property to a target property.
/// </summary>
public sealed class ThenToPropertyOptions
{
    /// <summary>
    /// Creates a new instance of <see cref="ThenToPropertyOptions"/>.
    /// </summary>
    /// <param name="propertyResolutionOptions">The property resolution options.</param>
    /// <param name="targetProperty">The target property.</param>
    public ThenToPropertyOptions(ToPropertyResolutionOptions propertyResolutionOptions, PropertyInfo targetProperty)
    {
        PropertyResolutionOptions = propertyResolutionOptions;
        TargetProperty = targetProperty;
    }

    /// <summary>
    /// The property resolution options.
    /// </summary>
    public ToPropertyResolutionOptions PropertyResolutionOptions { get; }

    /// <summary>
    /// The target property.
    /// </summary>
    public PropertyInfo TargetProperty { get; }
    
    /// <summary>
    /// The inner target property mapping. 
    /// </summary>
    public ThenToPropertyOptions? ThenToProperty { get; private set; }
    
    /// <summary>
    /// The assignment strategy options.
    /// </summary>
    public AssignmentStrategyOptions? AssignmentStrategy { get; private set; }
    
    /// <summary>
    /// Continues the mapping of the source property to an internal property of the target property.
    /// </summary>
    /// <param name="targetProperty">The internal property.</param>
    /// <typeparam name="TNextProperty">The internal property type.</typeparam>
    /// <returns></returns>
    public ThenToPropertyOptions ThenTo<TNextProperty>(PropertyInfo targetProperty)
    {
        ThenToProperty = new(PropertyResolutionOptions, targetProperty);
        return ThenToProperty;
    }
    
    /// <summary>
    /// <para>
    ///     Get o create the <see cref="AssignmentStrategyOptions{TProperty}"/>.
    /// </para>
    /// <para>
    ///     When the <see cref="AssignmentStrategy"/> is null or not for the <typeparamref name="TProperty"/> type,
    ///     a new instance of the <see cref="AssignmentStrategyOptions{TProperty}"/> is created.
    /// </para>
    /// </summary>
    /// <typeparam name="TProperty">The type of the source property.</typeparam>
    /// <returns>The <see cref="AssignmentStrategyOptions{TProperty}"/>.</returns>
    public AssignmentStrategyOptions<TProperty> GetOrCreateAssignmentStrategyOptions<TProperty>()
    {
        return PropertyResolutionOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
    }
}