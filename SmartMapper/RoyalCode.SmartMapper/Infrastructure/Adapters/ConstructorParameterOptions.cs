using RoyalCode.SmartMapper.Exceptions;
using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

/// <summary>
/// <para>
///     Options for mapping of a source property to a constructor parameter.
/// </para>
/// </summary>
public class ConstructorParameterOptions : ParameterOptionsBase
{
    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="ConstructorParameterOptions"/>.
    /// </para>
    /// </summary>
    /// <param name="targetType">The type of the target.</param>
    /// <param name="property">The source property mapped to a target constructor parameter.</param>
    public ConstructorParameterOptions(Type targetType, PropertyInfo property)
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
