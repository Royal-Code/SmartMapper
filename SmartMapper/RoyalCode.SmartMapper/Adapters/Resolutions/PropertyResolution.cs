using RoyalCode.SmartMapper.Adapters.Options.Resolutions;
using RoyalCode.SmartMapper.Adapters.Resolvers.Available;
using RoyalCode.SmartMapper.Core.Resolutions;
using System.Reflection;

namespace RoyalCode.SmartMapper.Adapters.Resolutions;

/// <summary>
/// A resolution for a property mapped to a property.
/// </summary>
public class PropertyResolution : ResolutionBase
{
    /// <summary>
    /// Creates a new instance of <see cref="PropertyResolution"/> for failed resolution.
    /// </summary>
    /// <param name="failure">The failure of the resolution.</param>
    public PropertyResolution(ResolutionFailure failure)
    {
        Resolved = false;
        Failure = failure;
    }

    public PropertyResolution(
        AvailableSourceProperty availableSourceProperty, 
        PropertyInfo targetProperty,
        AssignmentStrategyResolution assignmentStrategyResolution)
    {
        Resolved = true;
        AvailableSourceProperty = availableSourceProperty;
        TargetProperty = targetProperty;
        AssignmentStrategyResolution = assignmentStrategyResolution;
        PropertyResolutionStrategy = ToPropertyResolutionStrategy.SetValue;

        availableSourceProperty.ResolvedBy(this);
    }

    public PropertyResolution(
        AvailableSourceProperty availableSourceProperty,
        PropertyInfo targetProperty,
        ToPropertyResolutionStrategy propertyResolutionStrategy,
        ResolutionBase thenResolution)
    {
        Resolved = true;
        AvailableSourceProperty = availableSourceProperty;
        TargetProperty = targetProperty;
        PropertyResolutionStrategy = propertyResolutionStrategy;
        ThenResolution = thenResolution;

        availableSourceProperty.ResolvedBy(this);
    }

    /// <summary>
    /// The available source property.
    /// </summary>
    public AvailableSourceProperty? AvailableSourceProperty { get; }

    /// <summary>
    /// The target parameter.
    /// </summary>
    public PropertyInfo? TargetProperty { get; }

    /// <summary>
    /// The resolution strategy for the target property.
    /// </summary>
    public ToPropertyResolutionStrategy PropertyResolutionStrategy { get; }

    /// <summary>
    /// <para>
    ///     The assignment strategy resolution.
    /// </para>
    /// <para>
    ///     When the <see cref="PropertyResolutionStrategy"/> is <see cref="ToPropertyResolutionStrategy.SetValue"/>
    ///     this property will not be null, otherwise will be null.
    /// </para>
    /// </summary>
    public AssignmentStrategyResolution? AssignmentStrategyResolution { get; }

    /// <summary>
    /// <para>
    ///     Resolution for <see cref="ToPropertyResolutionStrategy.AccessInnerProperty"/>
    ///     and <see cref="ToPropertyResolutionStrategy.CallMethod"/>.
    /// </para>
    /// </summary>
    public ResolutionBase ThenResolution { get; }

    /// <inheritdoc />
    public override void Completed()
    {
        AvailableSourceProperty?.Completed();
    }
}