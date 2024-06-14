using System.Reflection;
using RoyalCode.SmartMapper.Mapping.Options.Resolutions;

namespace RoyalCode.SmartMapper.Mapping.Options;

/// <summary>
/// <para>
///     Options for one property of the source object.
/// </para>
/// <para>
///     Contains the resolution for the mapping of the property.
/// </para>
/// </summary>
public sealed class PropertyOptions
{
    /// <summary>
    /// Creates a new instance of <see cref="PropertyOptions"/>.
    /// </summary>
    /// <param name="property">The property to map.</param>
    public PropertyOptions(PropertyInfo property)
    {
        Property = property;
    }

    /// <summary>
    /// The source property to map.
    /// </summary>
    public PropertyInfo Property { get; }

    /// <summary>
    /// The resolution options between this source property to some member of the destination.
    /// </summary>
    public ResolutionOptionsBase? ResolutionOptions { get; private set; }

    /// <summary>
    /// Set the resolution options that resolved the property.
    /// </summary>
    /// <param name="resolutionOptions"></param>
    internal void ResolvedBy(ResolutionOptionsBase resolutionOptions)
    {
        if (ResolutionOptions is not null)
            throw new InvalidOperationException(
                $"The property '{Property.Name}' is already resolved by '{ResolutionOptions.GetType().Name}'");

        ResolutionOptions = resolutionOptions;
    }

    /// <summary>
    /// <para>
    ///     Try get the <see cref="SourceOptions"/> of the inner properties.
    /// </para>
    /// <para>
    ///     The value will not be null if the <see cref="ResolutionOptions"/> is an instance of
    ///     <see cref="InnerPropertiesResolutionOptionsBase"/>. Otherwise, the value will be null.
    ///     The <see cref="ResolutionOptions"/> is set by the method <see cref="ResolvedBy"/>,
    ///     so the property must be resolved with some resolution that supports inner properties.
    /// </para>
    /// </summary>
    /// <returns></returns>
    internal SourceOptions? GetInnerPropertiesSourceOptions()
    {
        return ResolutionOptions is InnerPropertiesResolutionOptionsBase innerPropertiesResolutionOptions
            ? innerPropertiesResolutionOptions.InnerSourceOptions
            : null;
    }

    /// <summary>
    /// <para>
    ///     Resolve ignoring the property.
    /// </para>
    /// </summary>
    internal void IgnoreMapping()
    {
        // when created the resolution options, this property options will be resolved by the resolution.
        IgnoreResolutionOptions.Resolves(this);
    }
}