using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

public class DirectMethodNameHandler : INameHandler
{
    public bool Handle(MemberDiscoveryContext context, int index, [NotNullWhen(true)] out MemberResolver? resolver)
    {
        resolver = null;

        if (!context.Partitions.GetName(index, out var name))
            return false;

        var methods = context.Request.TargetMethods.ListAvailableMethods()
            .Where(m => m.Method.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (methods.Count is 0)
            return false;

        resolver = new CallMethodResolver(context.Request.SourceProperty, methods);
        return true;
    }
}

