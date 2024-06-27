using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;
using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

namespace RoyalCode.SmartMapper.Mapping.Discovery.PropertyToMethods;

/// <summary>
/// A request to discover a target method to map a source property.
/// </summary>
/// <param name="configurations">The mapper configurations.</param>
/// <param name="sourceItem">The source item.</param>
/// <param name="availableTargetMethods">The available target methods.</param>
public readonly struct PropertyToMethodRequest(
    MapperConfigurations configurations,
    SourceProperty sourceItem,
    AvailableTargetMethods availableTargetMethods)
{
    /// <summary>
    /// The mapper configurations.
    /// </summary>
    public MapperConfigurations Configurations { get; } = configurations;

    /// <summary>
    /// The source item.
    /// </summary>
    public SourceProperty SourceItem { get; } = sourceItem;

    /// <summary>
    /// The available target methods.
    /// </summary>
    public AvailableTargetMethods TargetMethods { get; } = availableTargetMethods;
}
