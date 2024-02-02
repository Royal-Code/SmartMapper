using RoyalCode.SmartMapper.Adapters.Options;

namespace RoyalCode.SmartMapper.Adapters.Resolutions;

/// <summary>
/// Represent a source property of the mapping and the current resolution state.
/// </summary>
public sealed class SourceItem
{
    /// <summary>
    /// Options for the source property.
    /// </summary>
    public PropertyOptions Options { get; private init; }

    /// <summary>
    /// The kind or status of the mapping of the property.
    /// </summary>
    public ResolutionStatus ResolutionStatus { get; private set; }

    /// <summary>
    /// Options of the strategy to be used to assign the property value to the destination counterpart.
    /// </summary>
    public AssignmentStrategyOptions? AssignmentStrategy { get; private set; }


    // PropertyResolution 
}
