namespace RoyalCode.SmartMapper.Core.Discovery.Assignment;

/// <summary>
/// A discovery component that is responsible for discovering the assignments for the mapper.
/// </summary>
public interface IAssignmentDiscovery
{
    /// <summary>
    /// <para>
    ///     Discover the assignments resolution for the source and target types.
    /// </para>
    /// </summary>
    /// <param name="request">A request for the assignment discovery process.</param>
    /// <returns>
    ///     The result of the assignment discovery process.
    /// </returns>
    AssignmentDiscoveryResult Discover(AssignmentDiscoveryRequest request);
}
