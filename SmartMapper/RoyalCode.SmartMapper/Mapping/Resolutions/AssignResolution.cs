using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;
using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

namespace RoyalCode.SmartMapper.Mapping.Resolutions;

/// <summary>
/// A property resolution for assign value from a source property to a target property.
/// </summary>
public sealed class AssignResolution : PropertyResolution
{
    /// <summary>
    /// Creates a new instance of <see cref="AssignResolution"/> for failed resolution.
    /// </summary>
    /// <param name="failure">The failure of the resolution.</param>
    public AssignResolution(ResolutionFailure failure) : base(failure) { }
    
    /// <summary>
    /// Creates a new instance of <see cref="AssignResolution"/> for successful resolution.
    /// </summary>
    /// <param name="availableSourceProperty">The available source property.</param>
    /// <param name="targetProperty">The target property.</param>
    /// <param name="assignmentStrategyResolution">The assignment strategy resolution.</param>
    public AssignResolution(
        AvailableSourceProperty availableSourceProperty,
        TargetProperty targetProperty,
        AssignmentStrategyResolution assignmentStrategyResolution) : base(availableSourceProperty, targetProperty)
    {
        AssignmentStrategyResolution = assignmentStrategyResolution;
    }
    
    /// <summary>
    /// Check if the resolution has a failure.
    /// </summary>
    [MemberNotNullWhen(false, nameof(AssignmentStrategyResolution), nameof(AvailableSourceProperty), nameof(TargetProperty))]
    public bool HasFailure([NotNullWhen(true)] out ResolutionFailure? failure)
    {
        failure = Failure;
        return !Resolved;
    }
    
    /// <summary>
    /// <para>
    ///     The assignment strategy resolution.
    /// </para>
    /// </summary>
    public AssignmentStrategyResolution? AssignmentStrategyResolution { get; }
}