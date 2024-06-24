using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Core.Discovery.Members;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

public abstract class MemberResolver
{
    public abstract MemberResolution CreateResolution(MapperConfigurations configurations);
}

