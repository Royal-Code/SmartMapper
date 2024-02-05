using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Adapters.Resolvers.Avaliables;
using RoyalCode.SmartMapper.Adapters.Resolutions.Targets;
using RoyalCode.SmartMapper.Adapters.Options;

namespace RoyalCode.SmartMapper.Adapters.Resolutions;

/// <summary>
/// A resolution for a parameter of a constructor of method mapped from a source property.
/// </summary>
public class ParameterResolution : ResolutionBase
{
    /// <summary>
    /// Creates a new instance of <see cref="ParameterResolution"/>.
    /// </summary>
    /// <param name="availableSourceProperty"></param>
    /// <param name="parameter"></param>
    /// <param name="assignmentResolution"></param>
    /// <param name="assignmentStrategyOptions"></param>
    /// <exception cref="ArgumentNullException">
    ///     Case any of the parameters are null.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     Case the assignment resolution is undefined.
    /// </exception>
    public ParameterResolution(
        AvailableSourceProperty availableSourceProperty,
        TargetParameter parameter,
        ValueAssignmentResolution assignmentResolution,
        AssignmentStrategyOptions assignmentStrategyOptions)
    {
        if (assignmentResolution is ValueAssignmentResolution.Undefined)
            throw new ArgumentException("The assignment resolution must be defined.", nameof(assignmentResolution));

        AvailableSourceProperty = availableSourceProperty ?? throw new ArgumentNullException(nameof(availableSourceProperty));
        Parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
        AssignmentResolution = assignmentResolution;
        AssignmentStrategyOptions = assignmentStrategyOptions ?? throw new ArgumentNullException(nameof(assignmentStrategyOptions));
    }


    /// <summary>
    /// The available source property.
    /// </summary>
    public AvailableSourceProperty AvailableSourceProperty { get; }
    
    /// <summary>
    /// The target parameter.
    /// </summary>
    public TargetParameter Parameter { get; init; }

    /// <summary>
    /// The resolution of the assignment.
    /// </summary>
    public ValueAssignmentResolution AssignmentResolution { get; init; }

    /// <summary>
    /// The options of the strategy used to assign the value of the source property to the destination property or parameter.
    /// </summary>
    public AssignmentStrategyOptions AssignmentStrategyOptions { get; init; }
}