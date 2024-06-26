using RoyalCode.SmartMapper.Core.Configurations;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

public abstract class MemberResolver
{
    public abstract MemberDiscoveryResult CreateResolution(MapperConfigurations configurations);
}

