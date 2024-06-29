using RoyalCode.SmartMapper.Core.Extensions;
using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Mapping.Resolutions;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

/// <summary>
/// 
/// </summary>
public class PropertyNameHandler : INameHandler
{
    /// <inheritdoc />
    public bool Handle(
        MemberDiscoveryContext context,
        int index,
        [NotNullWhen(true)] out MemberResolver? resolver)
    {
        resolver = null;

        // Try full name property match
        if (context.Partitions.GetName(index, out var name)
            && context.Request.TargetProperties
                .Where(p => !p.IsResolved)
                .Where(p => p.PropertyInfo.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                .HasSingle(out var property))
        {
            resolver = new AssignPropertyResolver(context.Request, property);
            return true;
        }

        // partial name property match
        for (var end = 1; end < context.Partitions.Parts.Length; end++)
        {
            if (context.Partitions.GetName(index, end, out name)
                && context.Request.TargetProperties
                    .Where(p => !p.IsResolved || p.Resolution is MemberAccessResolution)
                    .Where(p => p.PropertyInfo.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    .HasSingle(out property))
            {
                resolver = new NavigationPropertyResolver(context, property, context.Partitions.Parts.Length - end);
                return true;
            }
        }

        return false;
    }
}

