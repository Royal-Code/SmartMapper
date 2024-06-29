using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;
using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

public readonly struct MemberDiscoveryRequest(
    MapperConfigurations configurations,
    AvailableSourceProperty sourceProperty,
    IReadOnlyCollection<TargetMethod> targetMethods,
    IReadOnlyCollection<TargetProperty> targetProperties)
{
    public MapperConfigurations Configurations { get; } = configurations;
    public AvailableSourceProperty SourceProperty { get; } = sourceProperty;
    public IReadOnlyCollection<TargetMethod> TargetMethods { get; } = targetMethods;
    public IReadOnlyCollection<TargetProperty> TargetProperties { get; } = targetProperties;
}

