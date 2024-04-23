using System.Collections;

namespace RoyalCode.SmartMapper.Adapters.Resolvers.Avaliables;

/// <summary>
/// Contains all <see cref="AvailableProperty"/> that are available to be mapped.
/// </summary>
public class AvailableTargetProperties
{
    private readonly ICollection<AvailableProperty> availableProperties;
    
    /// <summary>
    /// Create a new instance of <see cref="AvailableTargetProperties"/>.
    /// </summary>
    /// <param name="targetType">The target type to be mapped.</param>
    public AvailableTargetProperties(Type targetType)
    {
        availableProperties = AvailableProperty.Create(targetType);
    }
    
    /// <summary>
    /// List the available properties that are not resolved.
    /// </summary>
    /// <returns>A collection of available properties that are not resolved.</returns>
    public IEnumerable<AvailableProperty> ListAvailableProperties()
    {
        return availableProperties.Where(p => !p.Resolved);
    }
}