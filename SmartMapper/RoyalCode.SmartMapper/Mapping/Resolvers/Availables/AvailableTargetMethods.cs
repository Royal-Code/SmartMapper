using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

namespace RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

/// <summary>
/// Contains all <see cref="AvailableMethod"/> that are available to be mapped.
/// </summary>
public sealed class AvailableTargetMethods
{
    private readonly IReadOnlyCollection<AvailableMethod> availableMethod;
    
    /// <summary>
    /// Create a new instance of <see cref="AvailableTargetMethods"/>.
    /// </summary>
    /// <param name="targetmethods">The collection of target type to be mapped.</param>
    public AvailableTargetMethods(IReadOnlyCollection<TargetMethod> targetmethods)
    {
        availableMethod = AvailableMethod.Create(targetmethods);
    }

    /// <summary>
    /// Get the available methods that are not resolved.
    /// </summary>
    /// <returns>
    ///     The available methods that are not resolved.
    /// </returns>
    public IEnumerable<AvailableMethod> ListAvailableMethods()
    {
        return availableMethod.Where(m => !m.Resolved);
    }
}
