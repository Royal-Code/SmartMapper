using RoyalCode.SmartMapper.Adapters.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Options.Resolutions;

/// <summary>
/// Resolution option for map a property to a method.
/// </summary>
public class PropertyToMethodResolutionOptions : ResolutionOptionsBase
{
    /// <summary>
    /// Creates a new instance of <see cref="PropertyToMethodResolutionOptions"/>.
    /// </summary>
    /// <param name="resolvedProperty">The resolved property.
    public PropertyToMethodResolutionOptions(PropertyOptions resolvedProperty, MethodOptions methodOptions) 
        : base(resolvedProperty)
    {
        Status = ResolutionStatus.Undefined;
        MethodOptions = methodOptions;
    }

    /// <summary>
    /// The method options.
    /// </summary>
    public MethodOptions MethodOptions { get; }

    /// <summary>
    /// <para>
    ///     The options for the method parameter.
    /// </para>
    /// <para>
    ///     The value will be informed when the <see cref="Strategy"/> be <see cref="ToMethodStrategy.Value"/>.
    /// </para>
    /// </summary>
    public ToMethodParameterOptions? ValueOptions { get; private set; }

    /// <summary>
    /// <para>
    ///     The inner property options.
    /// </para>
    /// <para>
    ///     The value will be informed when the <see cref="Strategy"/> be <see cref="ToMethodStrategy.InnerProperties"/>.
    /// </para>
    /// </summary>
    public InnerPropertiesOptions? InnerPropertiesOptions { get; private set; }

    /// <summary>
    /// The strategy to map the property to a target method.
    /// </summary>
    public ToMethodStrategy Strategy { get; internal set; }

    internal void MapAsParameter()
    {
        Status = ResolutionStatus.MappedToMethodParameter;
        ValueOptions = new ToMethodParameterOptions(MethodOptions, ResolvedProperty.Property);
    }

    internal void MapInnerParameters()
    {
        Status = ResolutionStatus.MappedToMethod;
    }
}
