using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

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
    /// <param name="targetProperties"></param>
    /// <returns></returns>
    public static ICollection<AvailableProperty> Create(IReadOnlyCollection<TargetProperty> targetProperties)
    {
        return targetProperties
            .Where(p => !p.IsResolved)
            .Select(p => new AvailableProperty(p))
            .ToList();
    }
    
    private AvailableTargetProperties? innerAvailableProperties;
    private AvailableTargetMethods? innerAvailableMethods;

    /// <summary>
    /// Create a new instance of <see cref="AvailableProperty"/>.
    /// </summary>
    /// <param name="property"></param>
    public AvailableProperty(TargetProperty property)
    {
        Property = property;
    }
    
    /// <summary>
    /// The property information.
    /// </summary>
    public TargetProperty Property { get; init; }

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
        innerAvailableProperties ??= new AvailableTargetProperties(Property.InnerProperties);
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
        innerAvailableMethods ??= new AvailableTargetMethods(Property.InnerMethods);
        return innerAvailableMethods;
    }

    /// <summary>
    /// Resolve the property with the given resolution.
    /// </summary>
    /// <param name="resolution">The resolution to resolve the property.</param>
    public void ResolvedBy(ResolutionBase resolution)
    {
        // TODO: acho que não deveria ter isso aqui.

        Resolution = resolution;
        Resolved = true;
    }
}