
namespace RoyalCode.SmartMapper.Adapters.Options.Resolutions;

/// <summary>
/// The base class for resolution options that contains inner properties.
/// </summary>
public abstract class InnerPropertiesResolutionOptionsBase : ResolutionOptionsBase
{
    /// <summary>
    /// <para>
    ///     Base constructor for the resolution options that contains inner properties.
    /// </para>
    /// <para>
    ///     Call <see cref="PropertyOptions.ResolvedBy(ResolutionOptionsBase)"/> to add the current instance
    ///     as a resolution of the source property.
    /// </para>
    /// </summary>
    /// <param name="resolvedProperty">The source property related to the assignment.</param>
    protected InnerPropertiesResolutionOptionsBase(PropertyOptions resolvedProperty) : base(resolvedProperty)
    {
        InnerSourceOptions = new SourceOptions(resolvedProperty.Property.PropertyType);
    }

    /// <summary>
    /// The source options for the inner properties.
    /// </summary>
    public SourceOptions InnerSourceOptions { get; }
}
