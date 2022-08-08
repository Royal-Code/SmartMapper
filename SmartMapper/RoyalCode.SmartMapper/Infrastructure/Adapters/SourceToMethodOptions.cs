using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using RoyalCode.SmartMapper.Extensions;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

/// <summary>
/// Options containing configuration for the mapping of a source type to a destination type method.
/// </summary>
public class SourceToMethodOptions : OptionsBase
{
    private ICollection<PropertyToParameterOptions>? propertyToParameterOptions;
    private ICollection<PropertyToParameterOptions>? selectedPropertyToParameterSequence;

    public MethodInfo? Method { get; internal set; }
    public string? MethodName { get; internal set; }
    public ParametersStrategy ParametersStrategy { get; internal set; }

    /// <summary>
    /// <para>
    ///     Remove every configuration over the method parameters.
    /// </para>
    /// <para>
    ///     For each option, the configuration will be removed.
    /// </para>
    /// </summary>
    public void ClearParameters()
    {
        propertyToParameterOptions?.ForEach(p => p.Reset());
        propertyToParameterOptions = null;
        selectedPropertyToParameterSequence = null;
    }

    /// <summary>
    /// <para>
    ///     Gets or create a property to parameter options.
    /// </para>
    /// <para>
    ///     There no validation over the property, where the property is not a property of the source object.
    ///     Any property will be valid.
    /// </para>
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns>The property to parameter options.</returns>
    public PropertyToParameterOptions GetPropertyToParameterOptions(PropertyInfo property)
    {
        propertyToParameterOptions ??= new List<PropertyToParameterOptions>();

        var propertyToParameterOptionsItem = propertyToParameterOptions.FirstOrDefault(x => x.Property == property);
        if (propertyToParameterOptionsItem is null)
        {
            propertyToParameterOptionsItem = new PropertyToParameterOptions(this, property);
            propertyToParameterOptions.Add(propertyToParameterOptionsItem);
        }

        return propertyToParameterOptionsItem;
    }
    
    /// <summary>
    /// <para>
    ///     Try to get the property to parameter options,
    ///     if configured,
    ///     then return the options and true,
    ///     otherwise return null and false.
    /// </para>
    /// </summary>
    /// <param name="property">The property.</param>
    /// <param name="options">The property to parameter options.</param>
    /// <returns>True if the property to parameter options is configured, otherwise false.</returns>
    public bool TryGetPropertyToParameterOptions(
        PropertyInfo property, 
        [NotNullWhen(true)] out PropertyToParameterOptions? options)
    {
        options = propertyToParameterOptions?.FirstOrDefault(x => x.Property == property);
        return options is not null;
    }

    /// <summary>
    /// Adds the property to parameter options to the selected property to parameter sequence.
    /// </summary>
    /// <param name="options">The property to parameter options.</param>
    public void AddPropertyToParameterSequence(PropertyToParameterOptions options)
    {
        selectedPropertyToParameterSequence ??= new List<PropertyToParameterOptions>();
        selectedPropertyToParameterSequence.Add(options);
    }
    
    /// <summary>
    /// <para>
    ///     Gets the selected property to parameter sequence.
    /// </para>
    /// </summary>
    /// <returns>The selected property to parameter sequence.</returns>
    public IReadOnlyList<PropertyToParameterOptions> GetSelectedPropertyToParameterSequence()
    {
        return (selectedPropertyToParameterSequence ?? Enumerable.Empty<PropertyToParameterOptions>())
            .ToImmutableArray();
    }
}