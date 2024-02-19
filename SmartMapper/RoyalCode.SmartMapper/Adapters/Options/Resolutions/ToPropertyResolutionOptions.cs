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
    public ToMethodOptions? ThenToMethod { get; set; }
    
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

    public ToMethodOptions ThenCall()
    {
        throw new NotImplementedException();
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

