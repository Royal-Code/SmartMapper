using System.Reflection;
using RoyalCode.SmartMapper.Exceptions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

public abstract class ParameterOptionsBase: TargetRelatedOptionsBase
{
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
        
        ParameterName = parameterName;

        ParameterNameConfigured();
    }
    
    protected abstract void ParameterNameConfigured();
}