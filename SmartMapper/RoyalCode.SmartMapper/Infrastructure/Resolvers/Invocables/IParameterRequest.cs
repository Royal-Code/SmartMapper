
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.Invocables;

/// <summary>
/// <para>
///     This interface represents a request for a parameter resolution.
/// </para>
/// </summary>
public interface IParameterRequest
{
    /// <summary>
    /// <para>
    ///     Try get an <see cref="ToParameterOptionsBase"/> configured for the parameter.
    /// </para>
    /// </summary>
    /// <param name="name">The name of the parameter.</param>
    /// <param name="options">The options, if found.</param>
    /// <returns>True if the options are found, false otherwise.</returns>
    bool TryGetParameterOptionsByName(string name, [NotNullWhen(true)] out ToParameterOptionsBase? options);

    /// <summary>
    /// <para>
    ///     Try get an available source property.
    /// </para>
    /// </summary>
    /// <param name="propertyName">The source property name.</param>
    /// <param name="property">The available property, if found.</param>
    /// <returns>True if the property is found, false otherwise.</returns>
    bool TryGetAvailableSourceProperty(string propertyName,
        [NotNullWhen(true)] out AvailableSourceProperty? property);
    
    /// <summary>
    /// <para>
    ///     Try get an available source property.
    /// </para>
    /// </summary>
    /// <param name="propertyInfo">The property information.</param>
    /// <param name="property">The available property, if found.</param>
    /// <returns>True if the property is found, false otherwise.</returns>
    bool TryGetAvailableSourceProperty(PropertyInfo propertyInfo,
        [NotNullWhen(true)] out AvailableSourceProperty? property);

    /// <summary>
    /// <para>
    ///     The target parameter to be resolved.
    /// </para>
    /// </summary>
    TargetParameter Parameter { get; }

    /// <summary>
    /// <para>
    ///     The configuration used to resolve the parameter.
    /// </para>
    /// </summary>
    ResolutionConfiguration Configuration { get; }
}
