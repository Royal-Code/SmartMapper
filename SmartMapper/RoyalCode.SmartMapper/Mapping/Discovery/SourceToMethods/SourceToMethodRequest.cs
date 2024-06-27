﻿using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;
using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

namespace RoyalCode.SmartMapper.Mapping.Discovery.SourceToMethods;

/// <summary>
///  A request to discover a target method to map source properties.
/// </summary>
/// <param name="configurations">The mapper configurations.</param>
/// <param name="sourceType">The source type.</param>
/// <param name="sourceItems">The source items.</param>
/// <param name="availableMethods">The available taget methods.</param>
public readonly struct SourceToMethodRequest(
    MapperConfigurations configurations,
    Type sourceType,
    IEnumerable<SourceProperty> sourceItems,
    IEnumerable<AvailableMethod> availableMethods)
{
    /// <summary>
    /// The mapper configurations.
    /// </summary>
    public MapperConfigurations Configurations { get; } = configurations;

    /// <summary>
    /// The source type.
    /// </summary>
    public Type SourceType { get; } = sourceType;

    /// <summary>
    /// The source items.
    /// </summary>
    public IEnumerable<SourceProperty> SourceItems { get; } = sourceItems;

    /// <summary>
    /// The available taget methods.
    /// </summary>
    public IEnumerable<AvailableMethod> AvailableMethods { get; } = availableMethods;
}
