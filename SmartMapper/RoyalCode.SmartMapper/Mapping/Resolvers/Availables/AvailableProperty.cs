using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

/// <summary>
/// <para>
///     A property that is available to be mapped.
/// </para>
/// <para>
///     These properties are relative to the properties of the target type.
/// </para>
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
    
    private AvailableTargetProperties? innerAvailableProperties;
    private AvailableTargetMethods? innerAvailableMethods;

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
    [MemberNotNullWhen(true, nameof(Resolution))]
    public bool Resolved { get; private set; }
    
    /// <summary>
    /// The resolution of the property.
    /// </summary>
    public ResolutionBase? Resolution { get; private set; }

    /// <summary>
    /// <para>
    ///     Gets the available internal properties of this property.
    /// </para>
    /// <para>
    ///     This can be used for "Then" mappings.
    /// </para>
    /// </summary>
    /// <returns></returns>
    public AvailableTargetProperties GetInnerAvailableProperties()
    {
        innerAvailableProperties ??= new AvailableTargetProperties(Info.PropertyType);
        return innerAvailableProperties;
    }

    /// <summary>
    /// <para>
    ///     Gets the available internal methods of this property.
    /// </para>
    /// <para>
    ///     This can be used for "Then" mappings.
    /// </para>
    /// </summary>
    /// <returns></returns>
    public AvailableTargetMethods GetInnerAvailableMethods()
    {
        innerAvailableMethods ??= new AvailableTargetMethods(Info.PropertyType);
        return innerAvailableMethods;
    }

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