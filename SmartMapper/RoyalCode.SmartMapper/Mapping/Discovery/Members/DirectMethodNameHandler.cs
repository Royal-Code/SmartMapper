using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

public class DirectMethodNameHandler : INameHandler
{
    public bool Handle(MemberDiscoveryName names, int index, [NotNullWhen(true)] out MemberResolver? resolver)
    {
        resolver = null;

        if (!names.Partitions.GetName(index, out var name))
            return false;

        var methods = names.Request.TargetMethods.ListAvailableMethods()
            .Where(m => m.Info.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (methods.Count is 0)
            return false;

        resolver = new CallMethodResolver(names.Request.SourceProperty, methods);
        return true;
    }
}

