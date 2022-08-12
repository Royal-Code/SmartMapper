using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Infrastructure.Core;
using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

/// <summary>
/// <para>
///     Options for define the target construtor.
/// </para>
/// </summary>
public class ConstructorOptions : OptionsBase
{
    private ICollection<ConstructorParameterOptions>? parametersOptions;

    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="ConstructorOptions"/>.
    /// </para>
    /// </summary>
    /// <param name="targetType">Target type to be constructed.</param>
    public ConstructorOptions(Type targetType)
    {
        TargetType = targetType;
    }

    public Type TargetType { get; }
    
    /// <summary>
    /// <para>
    ///     A value for select the constructor.
    /// </para>
    /// <para>
    ///     Defines a number of parameters for the constructor.
    /// </para>
    /// </summary>
    public int? NumberOfParameters { get; internal set; }

    /// <summary>
    /// <para>
    ///     A value for select the constructor.
    /// </para>
    /// <para>
    ///     Defines the parameter types for the constructor.
    /// </para>
    /// </summary>
    public Type[]? ParameterTypes { get; internal set; }

    /// <summary>
    /// Get the options for the property to be mapped to a constructor parameter.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns>Options for mapping the property to a constructor parameter.</returns>
    public ConstructorParameterOptions GetConstructorParameterOptions(PropertyInfo property)
    {
        parametersOptions ??= new List<ConstructorParameterOptions>();

        var options = parametersOptions.FirstOrDefault(p => p.Property == property);
        if (options is null)
        {
            options = new ConstructorParameterOptions(TargetType, property);
            parametersOptions.Add(options);
        }

        return options;
    }
    
    /// <summary>
    /// <para>
    ///     Try get the options for the property to be mapped to a constructor parameter.
    /// </para>
    /// <para>
    ///     The options will be returned if the property was configured to be mapped to a constructor parameter.
    /// </para>
    /// </summary>
    /// <param name="property">The property.</param>
    /// <param name="options">Options for mapping the property to a constructor parameter.</param>
    /// <returns>True if the property was configured to be mapped to a constructor parameter.</returns>
    public bool TryGetConstructorParameterOptions(
        PropertyInfo property, 
        [NotNullWhen(true)] out ConstructorParameterOptions? options)
    {
        options = parametersOptions?.FirstOrDefault(p => p.Property == property);
        return options != null;
    }
}