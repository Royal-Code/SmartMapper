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
    /// <param name="resolvedProperty">The resolved property.</param>
    /// <param name="methodOptions">The method options.</param>
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
    public ToMethodStrategy Strategy { get; private set; }

    internal ToMethodParameterOptions MapAsParameter()
    {
        if (Strategy is ToMethodStrategy.InnerProperties)
            throw new InvalidOperationException(
                $"The property '{ResolvedProperty.Property.Name}' of type " +
                $"'{ResolvedProperty.Property.DeclaringType?.Name}' was mapped as inner properties " +
                $"and it is not possible to map as value.");
            
        Status = ResolutionStatus.MappedToMethodParameter;
        Strategy = ToMethodStrategy.Value;
        ValueOptions = new ToMethodParameterOptions(MethodOptions, ResolvedProperty.Property);
        return ValueOptions;
    }

    internal InnerPropertiesOptions MapInnerParameters()
    {
        if (Strategy is ToMethodStrategy.Value)
            throw new InvalidOperationException(
                $"The property '{ResolvedProperty.Property.Name}' of type " +
                $"'{ResolvedProperty.Property.DeclaringType?.Name}' was mapped as value " +
                $"and it is not possible to map as inner properties.");
        
        Status = ResolutionStatus.MappedToMethod;
        Strategy = ToMethodStrategy.InnerProperties;
        InnerPropertiesOptions = new InnerPropertiesOptions(ResolvedProperty.Property);
        return InnerPropertiesOptions;
    }
}
