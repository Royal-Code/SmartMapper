using RoyalCode.SmartMapper.Adapters.Options.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Options;

/// <summary>
/// Options to resolve a source property to call a target method.
/// </summary>
[Obsolete("Use the resolution, PropertyToMethodResolutionOptions, instead.")]
public sealed class ToMethodOptions
{
    /// <summary>
    /// Creates a new instance of <see cref="ToMethodOptions"/>.
    /// </summary>
    /// <param name="propertyOptions">The source property Optons.</param>
    /// <param name="methodOptions">The method options.</param>
    public ToMethodOptions(PropertyOptions propertyOptions, MethodOptions methodOptions)
    {
        MethodOptions = methodOptions;
    }

    /// <summary>
    /// The method options.
    /// </summary>
    public MethodOptions MethodOptions { get; }

    /// <summary>
    /// The property resolution options.
    /// </summary>
    public ToMethodResolutionOptions PropertyResolutionOptions { get; }

    /// <summary>
    /// The source property options.
    /// </summary>
    public PropertyOptions SourcePropertyOptions => PropertyResolutionOptions.ResolvedProperty;

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
    public ToMethodStrategy Strategy { get; private set; }

    internal ToMethodParameterOptions MapAsParameter()
    {
        if (Strategy is ToMethodStrategy.InnerProperties)
            throw new InvalidOperationException(
                $"The property '{SourcePropertyOptions.Property.Name}' of type " +
                $"'{SourcePropertyOptions.Property.DeclaringType?.Name}' was mapped as inner properties " +
                $"and it is not possible to map as value.");

        Strategy = ToMethodStrategy.Value;
        ValueOptions = new ToMethodParameterOptions(MethodOptions, SourcePropertyOptions.Property);
        return ValueOptions;
    }

    internal InnerPropertiesOptions MapInnerParameters()
    {
        if (Strategy is ToMethodStrategy.Value)
            throw new InvalidOperationException(
                $"The property '{SourcePropertyOptions.Property.Name}' of type " +
                $"'{SourcePropertyOptions.Property.DeclaringType?.Name}' was mapped as value " +
                $"and it is not possible to map as inner properties.");

        Strategy = ToMethodStrategy.InnerProperties;
        InnerPropertiesOptions = new InnerPropertiesOptions(SourcePropertyOptions.Property);
        return InnerPropertiesOptions;
    }
}