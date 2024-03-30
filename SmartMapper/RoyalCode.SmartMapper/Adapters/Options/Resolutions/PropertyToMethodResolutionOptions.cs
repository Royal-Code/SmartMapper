using System.Reflection;
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
        
        // if method existis, validate if method has a single parameter
        if (MethodOptions.Method is not null && MethodOptions.Method.GetParameters().Length != 1)
        {
            throw new InvalidOperationException(
                $"The method '{MethodOptions.Method.Name}' of type '{MethodOptions.Method.DeclaringType?.Name}'" +
                $" does not have a single parameter, and to map the property '{ResolvedProperty.Property.Name}'" +
                $" to a method as value, the method must have a single parameter.");
        }

        Status = ResolutionStatus.MappedToMethodParameter;
        Strategy = ToMethodStrategy.Value;
        ValueOptions = MethodOptions.GetParameterOptions(ResolvedProperty.Property);
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

    /// <summary>
    /// <para>
    ///     Verify if this method resolution can accept a method.
    /// </para>
    /// <para>
    ///     The method can be accepted if the <see cref="MethodOptions.Method"/> is the same as the method
    ///     or if the <see cref="MethodOptions.MethodName"/> is the same as the method name.
    /// </para>
    /// <para>
    ///     If the method options not define any method, the method is accepted.
    /// </para>
    /// </summary>
    /// <param name="method">The method to be verified.</param>
    /// <returns>True if the method can be accepted, otherwise false.</returns>
    public bool CanAcceptMethod(MethodInfo method)
    {
        if (MethodOptions.Method is not null)
            return MethodOptions.Method == method;
        
        if (MethodOptions.MethodName is not null)
            return MethodOptions.MethodName == method.Name;

        return true;
    }
}
