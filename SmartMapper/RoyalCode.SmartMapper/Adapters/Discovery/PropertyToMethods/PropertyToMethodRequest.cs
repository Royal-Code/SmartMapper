using RoyalCode.SmartMapper.Adapters.Resolutions;
using RoyalCode.SmartMapper.Adapters.Resolvers.Available;
using RoyalCode.SmartMapper.Core.Configurations;

namespace RoyalCode.SmartMapper.Adapters.Discovery.PropertyToMethods;

public readonly struct PropertyToMethodRequest(
    MapperConfigurations configurations,
    SourceItem sourceItem,
    AvailableTargetProperties availableTargetProperties)
{
    public MapperConfigurations Configurations { get; } = configurations;

    public SourceItem SourceItem { get; } = sourceItem;

    public AvailableTargetProperties TargetProperties { get; } = availableTargetProperties;
}
