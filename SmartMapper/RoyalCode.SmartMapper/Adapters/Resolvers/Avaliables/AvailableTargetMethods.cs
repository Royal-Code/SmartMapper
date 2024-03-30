namespace RoyalCode.SmartMapper.Adapters.Resolvers.Avaliables;

/// <summary>
/// Contains all <see cref="AvailableMethod"/> that are available to be mapped.
/// </summary>
public sealed class AvailableTargetMethods
{
    private readonly ICollection<AvailableMethod> availableMethod;
    
    /// <summary>
    /// Create a new instance of <see cref="AvailableTargetMethods"/>.
    /// </summary>
    /// <param name="targetType">The target type to be mapped.</param>
    public AvailableTargetMethods(Type targetType)
    {
        availableMethod = AvailableMethod.Create(targetType);
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
