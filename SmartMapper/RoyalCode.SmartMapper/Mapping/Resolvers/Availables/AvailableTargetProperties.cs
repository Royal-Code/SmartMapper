using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

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
    
    /// <summary>
    /// Try to find a available property by the given property info.
    /// </summary>
    /// <param name="propertyInfo">The property info to find.</param>
    /// <param name="availableProperty">The available property found, or null if the property was not available.</param>
    /// <returns>True if the property was found, otherwise false.</returns>
    public bool TryFindProperty(PropertyInfo propertyInfo, [NotNullWhen(true)] out AvailableProperty? availableProperty)
    {
        availableProperty = ListAvailableProperties()
            .FirstOrDefault(p => p.Info == propertyInfo);
        return availableProperty is not null;
    }
}