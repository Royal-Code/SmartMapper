using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Infrastructure.Core;
using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

/// <summary>
/// <para>
///     Options for mapping of a source property to a constructor parameter.
/// </para>
/// </summary>
public class PropertyToConstructorOptions : WithAssignmentOptionsBase
{
    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="PropertyToConstructorOptions"/>.
    /// </para>
    /// </summary>
    /// <param name="property"></param>
    public PropertyToConstructorOptions(Type targetType, PropertyInfo property)
    {
        TargetType = targetType;
        Property = property;
    }

    /// <summary>
    /// The target type that this property will be mapped.
    /// </summary>
    public Type TargetType { get; }

    /// <summary>
    /// The property that will be mapped to a method parameter.
    /// </summary>
    public PropertyInfo Property { get; }

    /// <summary>
    /// The parameter name.
    /// </summary>
    public string? ParameterName { get; private set; }

    /// <summary>
    /// Set the constructor parameter name to which this property should be mapped.
    /// </summary>
    /// <param name="parameterName">The name of the constructor parameter.</param>
    /// <exception cref="InvalidParameterNameException">
    ///     If none parameter exists in all constructors with the provided name.
    /// </exception>
    public void UseParameterName(string parameterName)
    {
        if (!TargetType.GetConstructors().Any(c => c.GetParameters().Any(p => p.Name == parameterName)))
            throw new InvalidParameterNameException(parameterName, TargetType, nameof(parameterName));

        ParameterName = parameterName;
    }
}
