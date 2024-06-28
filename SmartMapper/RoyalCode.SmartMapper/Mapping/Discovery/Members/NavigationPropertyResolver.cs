using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

/// <summary>
/// Resolves a navigation property. The source property name is split into parts and navigates to the target property.
/// </summary>
public sealed class NavigationPropertyResolver : MemberResolver
{
    private readonly AvailableProperty targetAvailableProperty;
    private readonly MemberDiscoveryContext names;
    private readonly int nameIndex;

    /// <summary>
    /// Creates a new instance of <see cref="NavigationPropertyResolver"/>.
    /// </summary>
    /// <param name="names">The name of the property.</param>
    /// <param name="targetAvailableProperty">The target property to resolve.</param>
    /// <param name="nameIndex">The index of the name.</param>
    public NavigationPropertyResolver(
        MemberDiscoveryContext names,
        AvailableProperty targetAvailableProperty,
        int nameIndex)
    {
        this.targetAvailableProperty = targetAvailableProperty;
        this.names = names;
        this.nameIndex = nameIndex;
    }

    /// <inheritdoc />
    public override MemberDiscoveryResult CreateResolution(MapperConfigurations configurations)
    {
        var innerContext = names.Inner(targetAvailableProperty);
        if (innerContext.HandleNextPart(nameIndex, out var thenResolver))
        {
            var thenResolution = thenResolver.CreateResolution(configurations);
            if (!thenResolution.IsResolved)
                return new()
                {
                    IsResolved = false,
                    Failure = thenResolution.Failure
                };
            
            var propertyResolution = new PropertyResolution(
                names.Request.SourceProperty,
                targetAvailableProperty.Property,
                thenResolution.Resolution);
            
            targetAvailableProperty.ResolvedBy(propertyResolution);
            
            return new()
            {
                IsResolved = true,
                Resolution = propertyResolution
            };
        }

        return new()
        {
            IsResolved = false,
            Failure = new ResolutionFailure(
                $"Could not resolve the property '{targetAvailableProperty.Property.Name}'")
        };
    }
}

