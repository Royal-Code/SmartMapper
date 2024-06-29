using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;
using RoyalCode.SmartMapper.Core.Discovery.Assignment;
using RoyalCode.SmartMapper.Mapping.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

/// <summary>
/// Resolves a single name property. The source property name matches the target property name.
/// </summary>
public sealed class AssignPropertyResolver : MemberResolver
{
    private readonly MemberDiscoveryRequest request;
    private readonly TargetProperty targetProperty;

    /// <summary>
    /// Creates a new <see cref="AssignPropertyResolver"/>.
    /// </summary>
    /// <param name="request">The discovery request.</param>
    /// <param name="targetProperty">The target property.</param>
    public AssignPropertyResolver(MemberDiscoveryRequest request, TargetProperty targetProperty)
    {
        this.request = request;
        this.targetProperty = targetProperty;
    }

    /// <inheritdoc />
    public override MemberDiscoveryResult CreateResolution(MapperConfigurations configurations)
    {
        var assignDiscoveryRequest = new AssignmentDiscoveryRequest(
            request.Configurations,
            request.SourceProperty.Options.Property.PropertyType,
            targetProperty.PropertyInfo.PropertyType);

        var assignDiscoveryResult = request.Configurations.Discovery.Assignment.Discover(assignDiscoveryRequest);

        if (!assignDiscoveryResult.IsResolved)
            return new()
            {
                IsResolved = false,
                Failure = assignDiscoveryResult.Failure
            };

        var resolution = new AssignResolution(
            request.SourceProperty,
            targetProperty,
            assignDiscoveryResult.Resolution);
        
        targetProperty.ResolvedBy(resolution);
        
        return new()
        {
            IsResolved = true,
            Resolution = resolution
        };
    }
}

