﻿using RoyalCode.SmartMapper.Adapters.Resolutions;
using RoyalCode.SmartMapper.Adapters.Resolvers.Available;
using RoyalCode.SmartMapper.Core.Configurations;

namespace RoyalCode.SmartMapper.Adapters.Discovery.PropertyToMethods;

/// <summary>
/// A request to discover a target method to map a source property.
/// </summary>
/// <param name="configurations">The mapper configurations.</param>
/// <param name="sourceItem">The source item.</param>
/// <param name="availableTargetMethods">The available target methods.</param>
public readonly struct PropertyToMethodRequest(
    MapperConfigurations configurations,
    SourceItem sourceItem,
    AvailableTargetMethods availableTargetMethods)
{
    /// <summary>
    /// The mapper configurations.
    /// </summary>
    public MapperConfigurations Configurations { get; } = configurations;

    /// <summary>
    /// The source item.
    /// </summary>
    public SourceItem SourceItem { get; } = sourceItem;

    /// <summary>
    /// The available target methods.
    /// </summary>
    public AvailableTargetMethods TargetMethods { get; } = availableTargetMethods;
}
