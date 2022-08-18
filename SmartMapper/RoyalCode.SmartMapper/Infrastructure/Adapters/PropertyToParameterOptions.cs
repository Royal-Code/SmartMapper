using System.Reflection;
using RoyalCode.SmartMapper.Exceptions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

/// <summary>
/// <para>
///     Options for mapping of a source property to a method parameter.
/// </para>
/// </summary>
public class PropertyToParameterOptions : ParameterOptionsBase
{
    // TODO: Renomear a classe para PropertyToMethodParamerterOptions ou MethodParameterOptions.
    // TODO: Dependendo do nome escolhido, deve ser alterado o nome do ConstructorParameterOptions também.

    /// <summary>
    /// Creates a new instance of <see cref="PropertyToParameterOptions"/>.
    /// </summary>
    /// <param name="methodOptions">The method options.</param>
    /// <param name="property">The property.</param>
    public PropertyToParameterOptions(SourceToMethodOptions methodOptions, PropertyInfo property)
    {
        MethodOptions = methodOptions;
        Property = property;
    }

    /// <summary>
    /// The method options.
    /// </summary>
    public SourceToMethodOptions MethodOptions { get; }

    /// <summary>
    /// The property that will be mapped to a method parameter.
    /// </summary>
    public PropertyInfo Property { get; }
    
    protected override void ParameterNameConfigured()
    {
        if (MethodOptions.Method is not null)
        {
            Parameter = MethodOptions.Method.GetParameters().FirstOrDefault(p => p.Name == ParameterName)
                        ?? throw new InvalidParameterNameException(ParameterName, MethodOptions.Method, nameof(ParameterName));
        }
    }
}