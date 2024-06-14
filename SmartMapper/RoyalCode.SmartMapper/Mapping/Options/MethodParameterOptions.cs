using RoyalCode.SmartMapper.Core.Exceptions;
using System.Reflection;

namespace RoyalCode.SmartMapper.Mapping.Options;

/// <summary>
/// <para>
///     Options for mapping of a source property to a method parameter.
/// </para>
/// </summary>
public sealed class MethodParameterOptions : ParameterOptionsBase
{
    /// <summary>
    /// Creates a new instance of <see cref="MethodParameterOptions"/>.
    /// </summary>
    /// <param name="methodOptions">The options for the method.</param>
    /// <param name="sourceProperty">The source property.</param>
    public MethodParameterOptions(MethodOptions methodOptions, PropertyInfo sourceProperty)
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
            Parameter = Array.Find(MethodOptions.Method.GetParameters(), p => p.Name == ParameterName)
                ?? throw new InvalidParameterNameException(ParameterName!, MethodOptions.Method, nameof(ParameterName));
        }
    }
}