using System.Reflection;
using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Resolvers.Avaliables;

/// <summary>
/// A property that is available to be mapped.
/// </summary>
public class AvailableProperty
{
    /// <summary>
    /// Create a collection of <see cref="AvailableProperty"/> from the target type.
    /// </summary>
    /// <param name="targetType"></param>
    /// <returns></returns>
    public static ICollection<AvailableProperty> Create(Type targetType)
    {
        return targetType.GetTypeInfo()
            .GetRuntimeProperties()
            .Where(p => p is { CanWrite: true })
            .Select(p => new AvailableProperty(p))
            .ToList();
    }
    
    /// <summary>
    /// Create a new instance of <see cref="AvailableProperty"/>.
    /// </summary>
    /// <param name="info"></param>
    public AvailableProperty(PropertyInfo info)
    {
        Info = info;
    }
    
    /// <summary>
    /// The property information.
    /// </summary>
    public PropertyInfo Info { get; init; }
    
    /// <summary>
    /// If the property is resolved.
    /// </summary>
    public bool Resolved { get; private set; }
    
    /// <summary>
    /// The resolution of the property.
    /// </summary>
    public ResolutionBase? Resolution { get; private set; }
    
    /// <summary>
    /// Resolve the property with the given resolution.
    /// </summary>
    /// <param name="resolution">The resolution to resolve the property.</param>
    public void ResolvedBy(ResolutionBase resolution)
    {
        Resolution = resolution;
        Resolved = true;
    }
}