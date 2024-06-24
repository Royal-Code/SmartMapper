using RoyalCode.SmartMapper.Core.Discovery.Members;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

public sealed class MemberDiscovery : IMemberDiscovery
{
    private readonly INameHandler[] nameHandlers = [];

    public MemberDiscoveryResult Discover(MemberDiscoveryRequest request)
    {
        var names = new NamePartitions(request.SourceProperty.Name;

        foreach (var handler in nameHandlers)
        {
            if (handler.Handle(request, names, 0, out var resolver))
                return new();
        }

        throw new NotImplementedException();
    }


}

