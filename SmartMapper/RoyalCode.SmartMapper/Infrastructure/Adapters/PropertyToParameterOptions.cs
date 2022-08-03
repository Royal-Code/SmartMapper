using System.Reflection;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

/// <summary>
/// <para>
///     Options for mapping of a source property to a method parameter.
/// </para>
/// </summary>
public class PropertyToParameterOptions : WithAssignmentOptionsBase
{
    /// <summary>
    /// Creates a new instance of <see cref="PropertyToParameterOptions"/>.
    /// </summary>
    /// <param name="methodOptions">The method options.</param>
    /// <param name="property">The property.</param>
    public PropertyToParameterOptions(AdapterSourceToMethodOptions methodOptions, PropertyInfo property)
    {
        MethodOptions = methodOptions;
        Property = property;
    }

    /// <summary>
    /// The method options.
    /// </summary>
    public AdapterSourceToMethodOptions MethodOptions { get; }

    /// <summary>
    /// The property that will be mapped to a method parameter.
    /// </summary>
    public PropertyInfo Property { get; }

    /// <summary>
    /// The parameter name.
    /// </summary>
    public string? ParameterName { get; private set; }
    
    /// <summary>
    /// <para>
    ///     The method parameter type.
    /// </para>
    /// <para>
    ///     This value can be determined if the method is defined and assigned the parameter name.
    /// </para>
    /// </summary>
    public ParameterInfo? Parameter { get; private set; }
    
    /// <summary>
    /// Configure the options for use the parameter name.
    /// </summary>
    /// <param name="parameterName">The parameter name.</param>
    /// <exception cref="ArgumentException">
    ///     Thrown if the parameter name is null or empty.
    ///     Thrown if the method does not have a parameter with the specified name.
    /// </exception>
    public void UseParameterName(string parameterName)
    {
        if (string.IsNullOrWhiteSpace(parameterName))
            throw new InvalidParameterNameException(nameof(parameterName));
        
        if (MethodOptions.Method is not null)
        {
            Parameter = MethodOptions.Method.GetParameters().FirstOrDefault(p => p.Name == parameterName)
                ?? throw new InvalidParameterNameException(parameterName, MethodOptions.Method, nameof(parameterName));
        }
        
        ParameterName = parameterName;
    }
}