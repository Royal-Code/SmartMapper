using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

public interface INameHandler
{
    public bool Handle(MemberDiscoveryRequest request, NamePartitions names, int index, [NotNullWhen(true)] out MemberResolver? resolver);
}

