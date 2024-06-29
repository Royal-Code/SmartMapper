namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

/// <summary>
/// Default implementation of the member discovery component.
/// </summary>
public sealed class MemberDiscovery : IMemberDiscovery
{
    private readonly INameHandler[] nameHandlers = [ new DirectMethodNameHandler(), new PropertyNameHandler() ];

    /// <inheritdoc />
    public MemberDiscoveryResult Discover(MemberDiscoveryRequest request)
    {
        var context = new MemberDiscoveryContext(request, nameHandlers);

        if (context.HandleNextPart(0, out var resolver))
            return resolver.CreateResolution(request.Configurations);
        
        return new()
        {
            IsResolved = false,
            Failure = new($"Could not resolve member {request.SourceProperty.SourceProperty.Options.Property.Name}")
        };
    }
}