using System.Reflection;
using RoyalCode.SmartMapper.Adapters.Resolutions;
using RoyalCode.SmartMapper.Mapping.Options;

namespace RoyalCode.SmartMapper.Adapters.Options.Resolutions;

/// <summary>
/// Options that contains the resolution of a property to a method parameter.
/// </summary>
public sealed class ToMethodParameterResolutionOptions : ParameterResolutionOptionsBase
{
    /// <summary>
    /// <para>
    ///     Creates a new <see cref="ToMethodParameterResolutionOptions"/> that resolves the source property.
    /// </para>
    /// <para>
    ///     Call <see cref="PropertyOptions.ResolvedBy(ResolutionOptionsBase)"/> to add the current instance
    ///     as a resolution of the source property.
    /// </para>
    /// </summary>
    /// <param name="resolvedProperty">The resolved source property options.</param>
    /// <param name="toParameterOptions">The parameter resolution options.</param>
    /// <returns>
    ///     A new instance of <see cref="ToMethodParameterResolutionOptions"/> that resolves the source property.
    /// </returns>
    public static ToMethodParameterResolutionOptions Resolves(
        PropertyOptions resolvedProperty,
        MethodParameterOptions toParameterOptions)
    {
        return new(resolvedProperty, toParameterOptions);
    }
    
    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="ToMethodParameterResolutionOptions"/>.
    /// </para>
    /// </summary>
    /// <param name="resolvedProperty">The resolved source property options.</param>
    /// <param name="toParameterOptions">The parameter resolution options.</param>
    private ToMethodParameterResolutionOptions(
        PropertyOptions resolvedProperty,
        MethodParameterOptions toParameterOptions) 
        : base(resolvedProperty, toParameterOptions)
    {
        ToMethodParameterOptions = toParameterOptions;
        Status = ResolutionStatus.MappedToMethodParameter;
    }

    /// <summary>
    /// The options for the method parameter.
    /// </summary>
    public MethodParameterOptions ToMethodParameterOptions { get; }

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
        if (ToMethodParameterOptions.MethodOptions.Method is not null)
            return ToMethodParameterOptions.MethodOptions.Method == method;
        
        if (ToMethodParameterOptions.MethodOptions.MethodName is not null)
            return ToMethodParameterOptions.MethodOptions.MethodName == method.Name;

        return true;
    }
}
