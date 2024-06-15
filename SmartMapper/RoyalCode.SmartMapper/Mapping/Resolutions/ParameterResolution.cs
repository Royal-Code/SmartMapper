using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

namespace RoyalCode.SmartMapper.Mapping.Resolutions;

/// <summary>
/// A resolution for a parameter of a constructor or method mapped from a source property.
/// </summary>
public class ParameterResolution : ResolutionBase
{
    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="ParameterResolution"/> for successful resolution.
    /// </para>
    /// <para>
    ///     Call the method <see cref="AvailableSourceProperty.ResolvedBy(ResolutionBase)"/>
    ///     to notify the source property that it has been resolved.
    /// </para>
    /// </summary>
    /// <param name="availableSourceProperty">The available source property that was resolved.</param>
    /// <param name="parameter">The target parameter that resolved the source property.</param>
    /// <param name="assignmentStrategyResolution">The assignment strategy resolution.</param>
    /// <returns>A new instance of <see cref="ParameterResolution"/>.</returns>
    /// <exception cref="ArgumentNullException">
    ///     Case any of the parameters are null.
    /// </exception>
    public static ParameterResolution Resolves(
        AvailableSourceProperty availableSourceProperty,
        AvailableParameter parameter,
        AssignmentStrategyResolution assignmentStrategyResolution)
    {
        return new ParameterResolution(availableSourceProperty, parameter, assignmentStrategyResolution);
    }
    
    /// <summary>
    /// Creates a new instance of <see cref="ParameterResolution"/> for successful resolution.
    /// </summary>
    /// <param name="availableSourceProperty"></param>
    /// <param name="parameter"></param>
    /// <param name="assignmentStrategyResolution"></param>
    /// <exception cref="ArgumentNullException">
    ///     Case any of the parameters are null.
    /// </exception>
    private ParameterResolution(
        AvailableSourceProperty availableSourceProperty,
        AvailableParameter parameter,
        AssignmentStrategyResolution assignmentStrategyResolution)
    {
        Resolved = true;
        AvailableSourceProperty = availableSourceProperty ?? throw new ArgumentNullException(nameof(availableSourceProperty));
        Parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
        AssignmentStrategyResolution = assignmentStrategyResolution ?? throw new ArgumentNullException(nameof(assignmentStrategyResolution));

        AvailableSourceProperty.ResolvedBy(this);
    }

    /// <summary>
    /// Creates a new instance of <see cref="ParameterResolution"/> for failed resolution.
    /// </summary>
    /// <param name="failure">The failure of the resolution.</param>
    public ParameterResolution(ResolutionFailure failure) 
    {
        Resolved = false;
        Failure = failure;
    }

    /// <summary>
    /// Check if the resolution has a failure.
    /// </summary>
    [MemberNotNullWhen(false, nameof(AvailableSourceProperty), nameof(Parameter), nameof(AssignmentStrategyResolution))]
    public bool HasFailure([NotNullWhen(true)] out ResolutionFailure? failure)
    {
        failure = Failure;
        return !Resolved;
    }
    
    /// <summary>
    /// The available source property.
    /// </summary>
    public AvailableSourceProperty? AvailableSourceProperty { get; }
    
    /// <summary>
    /// The target parameter.
    /// </summary>
    public AvailableParameter? Parameter { get; }

    /// <summary>
    /// The assignment strategy resolution.
    /// </summary>
    public AssignmentStrategyResolution? AssignmentStrategyResolution { get; }

    /// <inheritdoc />
    public override void Completed()
    {
        AvailableSourceProperty?.Completed();
    }
}