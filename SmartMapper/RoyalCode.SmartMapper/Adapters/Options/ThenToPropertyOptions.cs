using System.Reflection;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;
using RoyalCode.SmartMapper.Mapping.Options;

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
    public ThenToPropertyOptions(
        ToPropertyResolutionOptions propertyResolutionOptions, 
        ToTargetPropertyOptions targetProperty)
    {
        PropertyResolutionOptions = propertyResolutionOptions;
        TargetProperty = targetProperty;
    }

    /// <summary>
    /// The property resolution options.
    /// </summary>
    public ToPropertyResolutionOptions PropertyResolutionOptions { get; }

    /// <summary>
    /// The source property options.
    /// </summary>
    public PropertyOptions SourcePropertyOptions => PropertyResolutionOptions.ResolvedProperty;
    
    /// <summary>
    /// The target property.
    /// </summary>
    public ToTargetPropertyOptions TargetProperty { get; }
    
    /// <summary>
    /// The resolution strategy for the target property.
    /// </summary>
    public ToPropertyResolutionStrategy Strategy { get; private set; } = ToPropertyResolutionStrategy.SetValue;
    
    /// <summary>
    /// <para>
    ///     The inner target property mapping.
    /// </para>
    /// <para>
    ///     This property is not null when the <see cref="Strategy"/> is <see cref="ToPropertyResolutionStrategy.AccessInnerProperty"/>.
    /// </para>
    /// </summary>
    public ThenToPropertyOptions? ThenToProperty { get; private set; }

    /// <summary>
    /// <para>
    ///     The method mapping.
    /// </para>
    /// <para>
    ///     This property is not null when the <see cref="Strategy"/> is <see cref="ToPropertyResolutionStrategy.CallMethod"/>.
    /// </para>
    /// </summary>
    public ThenToMethodOptions? ThenToMethod { get; set; }
    
    /// <summary>
    /// <para>
    ///     The assignment strategy options.
    /// </para>
    /// <para>
    ///     The assignment strategy is used to define how the source property value will be assigned to the target property.
    /// </para>
    /// <para>
    ///     This property can be not null when the <see cref="Strategy"/> is <see cref="ToPropertyResolutionStrategy.SetValue"/>.
    /// </para>
    /// </summary>
    public AssignmentStrategyOptions? AssignmentStrategy { get; private set; }

    /// <summary>
    /// Continues the mapping of the source property to an internal property of the target property.
    /// </summary>
    /// <param name="targetProperty">The internal property.</param>
    /// <returns>
    ///     The <see cref="ThenToProperty"/> created.
    /// </returns>
    public ThenToPropertyOptions ThenTo(PropertyInfo targetProperty)
    {
        GuardThen();

        var innerTargetProperty = TargetProperty.GetInnerProperty(targetProperty);
        ThenToProperty = new(PropertyResolutionOptions, innerTargetProperty);
        Strategy = ToPropertyResolutionStrategy.AccessInnerProperty;
        return ThenToProperty;
    }
    
    /// <summary>
    /// Continues the mapping of the source property to a method of the target property.
    /// </summary>
    /// <returns>
    ///     The <see cref="ThenToMethod"/> created.
    /// </returns>
    public ThenToMethodOptions ThenCall()
    {
        GuardThen();
        Strategy = ToPropertyResolutionStrategy.CallMethod;

        ThenToMethod = new(PropertyResolutionOptions.ResolvedProperty, TargetProperty);

        return ThenToMethod;
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
        if (Strategy is not ToPropertyResolutionStrategy.SetValue)
        {
            throw new InvalidOperationException(
                "The assignment strategy is only available when the resolution strategy is SetValue.");
        }
        
        return PropertyResolutionOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
    }

    private void GuardThen()
    {
        if (Strategy is not ToPropertyResolutionStrategy.SetValue 
            || AssignmentStrategy is not null)
        {
            throw new InvalidOperationException(
                $"The property resolution strategy was defied as {Strategy} and can not be changed");
        }
    }
}