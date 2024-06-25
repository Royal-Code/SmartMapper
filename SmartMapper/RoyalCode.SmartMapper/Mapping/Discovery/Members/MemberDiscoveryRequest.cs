﻿using System.Reflection;
using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

public readonly struct MemberDiscoveryRequest(
    MapperConfigurations configurations,
    AvailableSourceProperty sourceProperty,
    AvailableTargetMethods targetMethods,
    AvailableTargetProperties targetProperties)
{
    public MapperConfigurations Configurations { get; } = configurations;
    public AvailableSourceProperty SourceProperty { get; } = sourceProperty;
    public AvailableTargetMethods TargetMethods { get; } = targetMethods;
    public AvailableTargetProperties TargetProperties { get; } = targetProperties;
}

