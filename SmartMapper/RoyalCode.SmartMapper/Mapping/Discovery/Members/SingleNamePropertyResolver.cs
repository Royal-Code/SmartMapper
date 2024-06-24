﻿using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Core.Discovery.Members;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

public sealed class SingleNamePropertyResolver : MemberResolver
{
    private readonly MemberDiscoveryRequest request;
    private readonly AvailableProperty availableProperty;

    public SingleNamePropertyResolver(MemberDiscoveryRequest request, AvailableProperty availableProperty)
    {
        this.request = request;
        this.availableProperty = availableProperty ?? throw new ArgumentNullException(nameof(availableProperty));
    }

    public override MemberResolution CreateResolution(MapperConfigurations configurations)
    {
        throw new NotImplementedException();
    }
}

