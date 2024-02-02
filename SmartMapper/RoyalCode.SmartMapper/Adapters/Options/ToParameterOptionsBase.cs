using RoyalCode.SmartMapper.Core.Exceptions;
using System.Reflection;

namespace RoyalCode.SmartMapper.Adapters.Options;

/// <summary>
/// Base class for options where the source property is mapped to a method ou constructor parameter.
/// </summary>
public abstract class ToParameterOptionsBase : ResolutionOptions
{
    /// <summary>
    /// <para>
    ///     Constructor with the source property.
    /// </para>
    /// </summary>
    /// <param name="sourceProperty">The source property.</param>
    protected ToParameterOptionsBase(PropertyInfo sourceProperty)
    {
        SourceProperty = sourceProperty;
    }

    /// <summary>
    /// The source property mapped to a method or constructor parameter.
    /// </summary>
    public PropertyInfo SourceProperty { get; }
    
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
    public ParameterInfo? Parameter { get; protected set; }
    
    /// <summary>
    /// <para>
    ///     Configure the options for use the parameter name.
    /// </para>
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
        
        ParameterName = parameterName;

        ParameterNameConfigured();
    }
    
    /// <summary>
    /// Executed when the parameter name is configured.
    /// </summary>
    protected abstract void ParameterNameConfigured();
}