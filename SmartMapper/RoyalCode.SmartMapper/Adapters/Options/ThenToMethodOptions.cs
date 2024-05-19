using RoyalCode.SmartMapper.Adapters.Options.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Options;

/// <summary>
/// Options to resolve a source property to call a target method.
/// </summary>
public sealed class ThenToMethodOptions
{
    /// <summary>
    /// Creates a new instance of <see cref="ThenToMethodOptions"/>.
    /// </summary>
    public ThenToMethodOptions(
        PropertyOptions sourcePropertyOptions,
        ToTargetPropertyOptions toTargetPropertyOptions)
    {
        SourcePropertyOptions = sourcePropertyOptions;
        ToTargetPropertyOptions = toTargetPropertyOptions;

        PropertyTargetOptions = new(toTargetPropertyOptions.TargetProperty.PropertyType);
        MethodOptions = PropertyTargetOptions.CreateMethodOptions();
    }

    /// <summary>
    /// The target property options.
    /// </summary>
    public ToTargetPropertyOptions ToTargetPropertyOptions { get; }

    /// <summary>
    /// The target options for the target property.
    /// </summary>
    public TargetOptions PropertyTargetOptions { get; }

    /// <summary>
    /// The method options.
    /// </summary>
    public MethodOptions MethodOptions { get; }

    /// <summary>
    /// The source property options.
    /// </summary>
    public PropertyOptions SourcePropertyOptions { get; }

    /// <summary>
    /// <para>
    ///     The options for the method parameter.
    /// </para>
    /// <para>
    ///     The value will be informed when <see cref="Strategy"/> be <see cref="ToMethodStrategy.Value"/>.
    /// </para>
    /// </summary>
    public ToMethodParameterOptions? ParameterOptions { get; private set; }

    /// <summary>
    /// <para>
    ///     The inner property options.
    /// </para>
    /// <para>
    ///     The value will be informed when <see cref="Strategy"/> be <see cref="ToMethodStrategy.InnerProperties"/>.
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
        ParameterOptions = new ToMethodParameterOptions(MethodOptions, SourcePropertyOptions.Property);
        return ParameterOptions;
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