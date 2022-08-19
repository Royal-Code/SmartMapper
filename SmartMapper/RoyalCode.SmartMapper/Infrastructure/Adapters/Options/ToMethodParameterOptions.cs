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
    /// <param name="methodOptionsBase">The options for the method.</param>
    /// <param name="sourceProperty">The source property.</param>
    public ToMethodParameterOptions(MethodOptionsBase methodOptionsBase, PropertyInfo sourceProperty)
        : base(sourceProperty)
    {
        MethodOptionsBase = methodOptionsBase;
    }
    
    /// <summary>
    /// The options for the method.
    /// </summary>
    public MethodOptionsBase MethodOptionsBase { get; }
    
    /// <summary>
    /// If the method are defined, check if has a parameter with the defined name.
    /// </summary>
    /// <exception cref="InvalidParameterNameException">
    ///     Thrown if the method has no parameter with the defined name.
    /// </exception>
    protected override void ParameterNameConfigured()
    {
        if (MethodOptionsBase.Method is not null)
        {
            Parameter = MethodOptionsBase.Method.GetParameters().FirstOrDefault(p => p.Name == ParameterName)
                        ?? throw new InvalidParameterNameException(ParameterName, MethodOptionsBase.Method, nameof(ParameterName));
        }
    }
}