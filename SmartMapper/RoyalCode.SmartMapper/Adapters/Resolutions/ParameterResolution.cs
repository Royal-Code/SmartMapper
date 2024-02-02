using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Adapters.Resolvers.Avaliables;
using RoyalCode.SmartMapper.Adapters.Resolutions.Targets;

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
    public ParameterResolution(AvailableSourceProperty availableSourceProperty)
    {
        AvailableSourceProperty = availableSourceProperty;
    }

    /// <summary>
    /// The available source property.
    /// </summary>
    public AvailableSourceProperty AvailableSourceProperty { get; }
    
    /// <summary>
    /// The target parameter.
    /// </summary>
    public TargetParameter? Parameter { get; init; }

    /// <summary>
    /// The resolution of the assignment.
    /// </summary>
    public ValueAssignmentResolution? AssignmentResolution { get; init; }
}