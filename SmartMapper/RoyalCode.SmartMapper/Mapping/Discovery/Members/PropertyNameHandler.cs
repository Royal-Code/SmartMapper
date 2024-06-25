using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

public class PropertyNameHandler : INameHandler
{
    public bool Handle(
        MemberDiscoveryRequest request,
        NamePartitions names,
        int index,
        [NotNullWhen(true)] out MemberResolver? resolver)
    {
        resolver = null;

        for (var end = 0; end <= names.Parts.Length; end++)
        {
            if (!names.GetName(index, end, out var name))
                return false;

            var properties = request.TargetProperties.ListAvailableProperties()
                .Where(p => p.Info.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (properties.Count == 1)
            {
                resolver = index == 0 && end == 0
                    ? new SingleNamePropertyResolver(request, properties[0])
                    : new NavegationPropertyResolver(request, properties[0], names, names.Parts.Length - end);
                return true;
            }
        }

        return false;
    }
}

