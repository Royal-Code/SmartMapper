
namespace RoyalCode.SmartMapper.Mapping.Discovery.SourceToMethods;

/// <summary>
/// A discovery component that is responsible for discovering if the source object can be mapped to a target method.
/// </summary>
public interface ISourceToMethodDiscovery
{
    /// <summary>
    /// Discover the method to map source properties.
    /// </summary>
    /// <param name="request">The mapping request.</param>
    /// <returns>The result of the discovery process.</returns>
    SourceToMethodResult Discover(SourceToMethodRequest request);
}
