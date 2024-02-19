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
    /// <param name="targetProperty">The target property options.</param>
    public ToPropertyResolutionOptions(PropertyOptions resolvedProperty, ToTargetPropertyOptions targetProperty)
        : base(resolvedProperty)
    {
        Status = ResolutionStatus.MappedToProperty;
        TargetProperty = targetProperty;
    }
    
    /// <summary>
    /// The target property.
    /// </summary>
    public ToTargetPropertyOptions TargetProperty { get; }

    /// <summary>
    /// The resolution strategy for the target property.
    /// </summary>
    public ToPropertyResolutionStrategy Strategy { get; private set; } = ToPropertyResolutionStrategy.SetValue;

    /// <summary>
    /// The inner target property mapping.
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
    /// <returns>The options to resolve a source property to a target property.</returns>
    public ThenToPropertyOptions ThenTo(PropertyInfo targetProperty)
    {
        var innerTargetProperty = TargetProperty.GetInnerProperty(targetProperty);
        ThenToProperty = new(this, innerTargetProperty);
        Strategy = ToPropertyResolutionStrategy.AccessInnerProperty;
        return ThenToProperty;
    }

    public ThenToMethodOptions ThenCall()
    {
        throw new NotImplementedException();
    }
}

