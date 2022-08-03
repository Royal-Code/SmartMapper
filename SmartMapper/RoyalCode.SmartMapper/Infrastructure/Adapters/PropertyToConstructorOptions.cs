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
    public PropertyToConstructorOptions(PropertyInfo property)
    {
        Property = property;
    }
    
    /// <summary>
    /// The property that will be mapped to a method parameter.
    /// </summary>
    public PropertyInfo Property { get; }

    /// <summary>
    /// The parameter name.
    /// </summary>
    public string? ParameterName { get; private set; }

    internal void UseParameterName(string parameterName)
    {
        throw new NotImplementedException();
    }
}
