using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;
using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

namespace RoyalCode.SmartMapper.Mapping.Resolutions;

/// <summary>
/// <para>
///     A resolution for mapping a source property to a method of a target property,
///     where the source property is mapped to a parameter of the method.
/// </para>
/// <para>
///     The method must have a single parameter that can be resolved from the source property.
/// </para>
/// </summary>
public sealed class PropertyToParameterResolution : PropertyResolution
{
    /// <summary>
    /// Constructor for failed resolution of <see cref="PropertyToParameterResolution"/> .
    /// </summary>
    /// <param name="failure">The failure of the resolution.</param>
    public PropertyToParameterResolution(ResolutionFailure failure) : base(failure) { }

    /// <summary>
    /// Constructor for successful resolution of <see cref="PropertyToParameterResolution"/> .
    /// </summary>
    /// <param name="method">The resolved method information.</param>
    /// <param name="assignmentStrategyResolution">The assignment strategy resolution.</param>
    /// <param name="availableSourceProperty">The available source property.</param>
    /// <param name="targetProperty">The target property.</param>
    public PropertyToParameterResolution(
        TargetMethod method,
        AssignmentStrategyResolution assignmentStrategyResolution,
        AvailableSourceProperty availableSourceProperty,
        TargetProperty targetProperty) : base(availableSourceProperty, targetProperty)
    {
        Method = method;
        AssignmentStrategyResolution = assignmentStrategyResolution;
    }
    
    /// <summary>
    /// The resolved method information.
    /// </summary>
    public TargetMethod? Method { get; }
    
    /// <summary>
    /// <para>
    ///     The assignment strategy resolution.
    /// </para>
    /// </summary>
    public AssignmentStrategyResolution? AssignmentStrategyResolution { get; }

    /// <summary>
    /// Check if the resolution has a failure.
    /// </summary>
    [MemberNotNullWhen(false, nameof(Method), nameof(AssignmentStrategyResolution), nameof(AvailableSourceProperty), nameof(TargetProperty))]
    public bool HasFailure([NotNullWhen(true)] out ResolutionFailure? failure)
    {
        failure = Failure;
        return !Resolved;
    }
    
    /// <inheritdoc />
    public override void Completed()
    {
        base.Completed();
        Method?.ResolvedBy(this);
    }
}