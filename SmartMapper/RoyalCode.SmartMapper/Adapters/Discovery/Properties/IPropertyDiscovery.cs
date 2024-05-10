namespace RoyalCode.SmartMapper.Adapters.Discovery.Properties;

/// <summary>
/// A discovery component that is responsible for discovering if a source property can be mapped
/// to a target property.
/// </summary>
public interface IPropertyDiscovery
{
    /// <summary>
    /// A discovery component that is responsible for discovering if a source property can be mapped
    /// to a target property.
    /// </summary>
    /// <param name="request">The mapping request.</param>
    /// <returns>The result of the discovery process.</returns>
    PropertyResult Discover(PropertyRequest request);
}