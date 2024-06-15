
namespace RoyalCode.SmartMapper.Mapping.Discovery.PropertyToMethods;

/// <summary>
/// A discovery component that is responsible for discovering if a source property can be mapped 
/// to a method of a target property.
/// </summary>
public interface IPropertyToMethodDiscovery
{
    /// <summary>
    /// A discovery component that is responsible for discovering if a source property can be mapped 
    /// to a method of a target property.
    /// </summary>
    /// <param name="request">The mapping request.</param>
    /// <returns>The result of the discovery process.</returns>
    PropertyToMethodResult Discover(PropertyToMethodRequest request);
}
