using System.Reflection;

namespace RoyalCode.SmartMapper.Mapping.Options.Resolutions;

/// <summary>
/// The options to resolve a source property to a target property.
/// </summary>
public sealed class ToPropertyResolutionOptions : ResolutionOptionsBase
{
    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="ToPropertyResolutionOptions"/>.
    /// </para>
    /// <para>
    ///     Call <see cref="PropertyOptions.ResolvedBy(ResolutionOptionsBase)"/> to add the current instance
    ///     as a resolution of the source property.
    /// </para>
    /// </summary>
    /// <param name="resolvedProperty">The resolved property options.</param>
    /// <param name="targetProperty">The target property options.</param>
    /// <returns>A new instance of <see cref="ToPropertyResolutionOptions"/>.</returns>
    public static ToPropertyResolutionOptions Resolves(
        PropertyOptions resolvedProperty, ToTargetPropertyOptions targetProperty)
    {
        return new(resolvedProperty, targetProperty);
    }
    
    /// <summary>
    /// Creates a new instance of <see cref="ToPropertyResolutionOptions"/>.
    /// </summary>
    /// <param name="resolvedProperty">The resolved property options.</param>
    /// <param name="targetProperty">The target property options.</param>
    private ToPropertyResolutionOptions(PropertyOptions resolvedProperty, ToTargetPropertyOptions targetProperty)
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
    /// Continues the mapping of the source property to an internal property of the target property.
    /// </summary>
    /// <param name="targetProperty">The internal property.</param>
    /// <returns>The options to resolve a source property to a target property.</returns>
    public ThenToPropertyOptions ThenTo(PropertyInfo targetProperty)
    {
        var innerTargetProperty = TargetProperty.GetInnerProperty(targetProperty);
        var thenToProperty = new ThenToPropertyOptions(this, innerTargetProperty);
        
        UseResolutionStrategy(ToPropertyResolutionStrategy.AccessInnerProperty);
        
        ThenToProperty = thenToProperty;
        return ThenToProperty;
    }

    /// <summary>
    /// Maps the source property to a method of the target property.
    /// </summary>
    /// <returns>
    ///     The options to map the source property to a method of the target property.
    /// </returns>
    public ThenToMethodOptions ThenCall()
    {
        UseResolutionStrategy(ToPropertyResolutionStrategy.CallMethod);

        ThenToMethod = new(ResolvedProperty, TargetProperty);
        return ThenToMethod;
    }

    private void UseResolutionStrategy(ToPropertyResolutionStrategy strategy)
    {
        if (Strategy == strategy) 
            return;

        if (Strategy != ToPropertyResolutionStrategy.SetValue || AssignmentStrategy is not null)
            throw new InvalidOperationException($"Another resolution strategy has already been used: '{Strategy}'.");

        Strategy = strategy;
    }

    /// <summary>
    /// Internal validation of assignment stragety options.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    protected override void GuardCreateAssignmentStrategyOptions()
    {
        if (Strategy is not ToPropertyResolutionStrategy.SetValue)
            throw new InvalidOperationException(
                $"To provide an assignment strategy, the resolution strategy must be 'SetValue', but currently it is '{Strategy}'.");
    }
}

