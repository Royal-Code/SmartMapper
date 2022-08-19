using System.Reflection;
using RoyalCode.SmartMapper.Exceptions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

/// <summary>
/// <para>
///     Options for mapping of a source property to a constructor parameter.
/// </para>
/// </summary>
public class ToConstructorParameterOptions : ToParameterOptionsBase
{
    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="ToConstructorParameterOptions"/>.
    /// </para>
    /// </summary>
    /// <param name="targetType">The target type.</param>
    /// <param name="sourceProperty">The source property.</param>
    public ToConstructorParameterOptions(Type targetType, PropertyInfo sourceProperty)
        : base(sourceProperty)
    {
        TargetType = targetType;
    }

    /// <summary>
    /// The target type of the constructor parameter.
    /// </summary>
    public Type TargetType { get; }
    
    /// <summary>
    /// Check if the target type has a constructor with the specified parameter name.
    /// </summary>
    /// <exception cref="InvalidParameterNameException"></exception>
    protected override void ParameterNameConfigured()
    {
        var constructors = TargetType.GetConstructors()
            .Where(c => c.IsPublic)
            .Where(c => c.GetParameters().Any(p => p.Name == ParameterName))
            .ToList();

        if (constructors.Count is 0)
            throw new InvalidParameterNameException(ParameterName!, TargetType, nameof(ParameterName));
    }
}