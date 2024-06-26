namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

#pragma warning disable CS1591 // XML doc.

/// <summary>
/// A discovery component that is responsible for discovering the mapping
/// from a source property to a destination member, that is a property or a method.
/// </summary>
public interface IMemberDiscovery
{
    /// <summary>
    /// Discover the mapping from a source property to a destination member.
    /// </summary>
    /// <param name="request">The discovery request.</param>
    /// <returns>The result of the member discovery process.</returns>
    MemberDiscoveryResult Discover(MemberDiscoveryRequest request);
}