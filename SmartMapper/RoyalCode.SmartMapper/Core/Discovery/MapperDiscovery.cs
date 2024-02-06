
using RoyalCode.SmartMapper.Adapters.Discovery.Parameters;
using RoyalCode.SmartMapper.Core.Discovery.Assignment;

namespace RoyalCode.SmartMapper.Core.Discovery;

/// <summary>
/// Contains all the discovery components for the mapper.
/// </summary>
public sealed class MapperDiscovery
{
    /// <summary>
    /// Component responsible for discovering the assignment strategy between a source value type and the target type.
    /// </summary>
    public IAssignmentDiscovery Assignment { get; private set; } = new DefaultAssignmentDiscovery();

    /// <summary>
    /// Component responsible for discovering the mapping between a property and a parameter.
    /// </summary>
    public IToParameterDiscovery ToParameter { get; private set; } = new DefaultToParameterDiscovery();

    /// <summary>
    /// Set the assignment discovery component for the mapper.
    /// </summary>
    /// <param name="assignmentDiscovery">The assignment discovery component.</param>
    public void SetAssignmentDiscovery(IAssignmentDiscovery assignmentDiscovery)
    {
        if (assignmentDiscovery is null)
            throw new ArgumentNullException(nameof(assignmentDiscovery));

        Assignment = assignmentDiscovery;
    }
}
