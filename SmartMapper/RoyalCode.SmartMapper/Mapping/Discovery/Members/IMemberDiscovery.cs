using RoyalCode.SmartMapper.Mapping.Discovery.Members;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

namespace RoyalCode.SmartMapper.Core.Discovery.Members;

#pragma warning disable CS1591 // XML doc.

public class MemberResolution
{

}

public interface IMemberDiscovery
{
    MemberDiscoveryResult Discover(MemberDiscoveryRequest request);
}

public readonly struct MemberDiscoveryResult
{

    public AvailableMethod? Method { get; }

    public AvailableProperty? Property { get; }
}

