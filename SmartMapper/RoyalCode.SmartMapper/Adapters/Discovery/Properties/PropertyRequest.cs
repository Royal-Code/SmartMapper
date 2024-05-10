using RoyalCode.SmartMapper.Adapters.Resolutions;
using RoyalCode.SmartMapper.Adapters.Resolvers.Available;
using RoyalCode.SmartMapper.Core.Configurations;

namespace RoyalCode.SmartMapper.Adapters.Discovery.Properties;

/// <summary>
/// A request to discover a target property to map source properties.
/// </summary>
/// <param name="configurations">The mapper configurations.</param>
/// <param name="sourceItem">The source item.</param>
/// <param name="availableTargetProperties">The available target properties.</param>
public readonly struct PropertyRequest(
    MapperConfigurations configurations,
    SourceItem sourceItem,
    AvailableTargetProperties availableTargetProperties)
{
    /// <summary>
    /// The mapper configurations.
    /// </summary>
    public MapperConfigurations Configurations { get; } = configurations;

    /// <summary>
    /// The source item to be mapped.
    /// </summary>
    public SourceItem SourceItem { get; } = sourceItem;

    /// <summary>
    /// The target available properties to be mapped.
    /// </summary>
    public AvailableTargetProperties TargetProperties { get; } = availableTargetProperties;
    
}