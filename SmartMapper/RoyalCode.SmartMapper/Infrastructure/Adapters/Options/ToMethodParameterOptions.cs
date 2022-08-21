using System.Reflection;
using RoyalCode.SmartMapper.Exceptions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

/// <summary>
/// <para>
///     Options for mapping of a source property to a method parameter.
/// </para>
/// </summary>
public class ToMethodParameterOptions : ToParameterOptionsBase
{
    /// <summary>
    /// Creates a new instance of <see cref="ToMethodParameterOptions"/>.
    /// </summary>
    /// <param name="methodOptions">The options for the method.</param>
    /// <param name="sourceProperty">The source property.</param>
    public ToMethodParameterOptions(MethodOptions methodOptions, PropertyInfo sourceProperty)
        : base(sourceProperty)
    {
        MethodOptions = methodOptions;
    }
    
    /// <summary>
    /// The options for the method.
    /// </summary>
    public MethodOptions MethodOptions { get; }
    
    /// <summary>
    /// If the method are defined, check if has a parameter with the defined name.
    /// </summary>
    /// <exception cref="InvalidParameterNameException">
    ///     Thrown if the method has no parameter with the defined name.
    /// </exception>
    protected override void ParameterNameConfigured()
    {
        if (MethodOptions.Method is not null)
        {
            Parameter = MethodOptions.Method.GetParameters().FirstOrDefault(p => p.Name == ParameterName)
                        ?? throw new InvalidParameterNameException(ParameterName, MethodOptions.Method, nameof(ParameterName));
        }
    }
}