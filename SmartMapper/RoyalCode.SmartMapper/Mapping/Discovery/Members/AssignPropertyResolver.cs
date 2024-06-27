using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;
using RoyalCode.SmartMapper.Core.Discovery.Assignment;
using RoyalCode.SmartMapper.Mapping.Resolutions;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

/// <summary>
/// Resolves a single name property. The source property name matches the target property name.
/// </summary>
public sealed class AssignPropertyResolver : MemberResolver
{
    private readonly MemberDiscoveryRequest request;
    private readonly AvailableProperty targetAvailableProperty;

    /// <summary>
    /// Creates a new <see cref="AssignPropertyResolver"/>.
    /// </summary>
    /// <param name="request">The discovery request.</param>
    /// <param name="targetAvailableProperty">The target property.</param>
    public AssignPropertyResolver(MemberDiscoveryRequest request, AvailableProperty targetAvailableProperty)
    {
        this.request = request;
        this.targetAvailableProperty = targetAvailableProperty;
    }

    /// <inheritdoc />
    public override MemberDiscoveryResult CreateResolution(MapperConfigurations configurations)
    {
        var assignDiscoveryRequest = new AssignmentDiscoveryRequest(
            request.Configurations,
            request.SourceProperty.Options.Property.PropertyType,
            targetAvailableProperty.Info.PropertyType);

        var assignDiscoveryResult = request.Configurations.Discovery.Assignment.Discover(assignDiscoveryRequest);

        if (!assignDiscoveryResult.IsResolved)
            return new()
            {
                IsResolved = false,
                Failure = assignDiscoveryResult.Failure
            };

        var resolution = new PropertyResolution(
            request.SourceProperty,
            targetAvailableProperty.Info,
            assignDiscoveryResult.Resolution);
        
        targetAvailableProperty.ResolvedBy(resolution);
        
        return new()
        {
            IsResolved = true,
            Resolution = resolution
        };
    }
}

