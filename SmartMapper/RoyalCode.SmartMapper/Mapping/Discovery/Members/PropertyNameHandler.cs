using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

public class PropertyNameHandler : INameHandler
{
    public bool Handle(
        MemberDiscoveryName names,
        int index,
        [NotNullWhen(true)] out MemberResolver? resolver)
    {
        resolver = null;

        for (var end = 0; end <= names.Partitions.Parts.Length; end++)
        {
            if (!names.Partitions.GetName(index, end, out var name))
                return false;

            var properties = names.Request.TargetProperties.ListAvailableProperties()
                .Where(p => p.Info.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (properties.Count == 1)
            {
                resolver = index == 0 && end == 0
                    ? new SingleNamePropertyResolver(names.Request, properties[0])
                    : new NavigationPropertyResolver(names, properties[0], names.Partitions.Parts.Length - end);
                return true;
            }
        }

        return false;
    }
}

