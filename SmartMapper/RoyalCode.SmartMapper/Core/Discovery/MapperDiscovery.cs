
using RoyalCode.SmartMapper.Core.Configurations;

namespace RoyalCode.SmartMapper.Core.Discovery;

/// <summary>
/// Contains all the discovery components for the mapper.
/// </summary>
public sealed class MapperDiscovery
{
    private readonly MapperConfigurations configurations;

    /// <summary>
    /// Create a new instance of <see cref="MapperDiscovery"/>.
    /// </summary>
    /// <param name="configurations">The configurations for the mapper.</param>
    public MapperDiscovery(MapperConfigurations configurations)
    {
        this.configurations = configurations;
    }

    /// <summary>
    /// Discovery component that is responsible for discovering the assignments for the mapper.
    /// </summary>
    public IAssignmentDiscovery Assignment { get; private set; } = new InternalAssignmentDiscovery();

    /// <summary>
    /// Set the assignment discovery component for the mapper.
    /// </summary>
    /// <param name="assignmentDiscovery">The assignment discovery component.</param>
    public void SetAssignmentDiscovery(IAssignmentDiscovery assignmentDiscovery)
    {
        if (assignmentDiscovery is null)
            throw new ArgumentNullException(nameof(assignmentDiscovery));

        assignmentDiscovery.Configurations = configurations;
        Assignment = assignmentDiscovery;
    }
}

internal sealed class InternalAssignmentDiscovery : IAssignmentDiscovery
{
    public MapperConfigurations Configurations { get; set; }

    public AssignmentDiscoveryResult Discover(Type sourceType, Type targetType)
    {
        return AssignmentDiscoveryResult.Fail;
    }
}

/// <summary>
/// A discovery component that is responsible for discovering the assignments for the mapper.
/// </summary>
public interface IAssignmentDiscovery
{
    /// <summary>
    /// <para>
    ///     The configurations for the mapper.
    /// </para>
    /// <para>
    ///     When the discovery is executed, it will use the configurations to discover the assignments.
    /// </para>
    /// </summary>
    MapperConfigurations Configurations { get; set; }

    /// <summary>
    /// Discover the assignments for the mapper.
    /// </summary>
    /// <param name="sourceType">The value type of the source property.</param>
    /// <param name="targetType">The value type of the target property or parameter.</param>
    /// <returns></returns>
    AssignmentDiscoveryResult Discover(Type sourceType, Type targetType);
}

public sealed class AssignmentDiscoveryResult
{
    internal static AssignmentDiscoveryResult Fail = new();
}