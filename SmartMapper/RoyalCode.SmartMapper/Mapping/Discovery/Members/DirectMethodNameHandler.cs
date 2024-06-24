using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

public class DirectMethodNameHandler : INameHandler
{
    public bool Handle(MemberDiscoveryRequest request, NamePartitions names, int index, [NotNullWhen(true)] out MemberResolver? resolver)
    {
        resolver = null;

        if (!names.GetName(index, out var name))
            return false;

        var methods = request.TargetMethods.ListAvailableMethods()
            .Where(m => m.Info.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (methods.Count is 0)
            return false;

        resolver = CallMethodResolver.Create(request.SourceProperty, methods);
        return true;
    }
}

