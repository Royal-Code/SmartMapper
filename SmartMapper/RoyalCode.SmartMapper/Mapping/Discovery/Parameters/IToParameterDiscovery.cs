
namespace RoyalCode.SmartMapper.Mapping.Discovery.Parameters;

/// <summary>
/// A discovery component that is responsible for discovering the property to parameter assignments for the mapper.
/// </summary>
public interface IToParameterDiscovery
{
    /// <summary>
    /// <para>
    ///     Discover the property to be mapped to the parameter.
    /// </para>
    /// </summary>
    /// <param name="request">The mapping request.</param>
    /// <returns>The result of the property to parameter discovery process.</returns>
    ToParameterDiscoveryResult Discover(ToParameterDiscoveryRequest request);
}
