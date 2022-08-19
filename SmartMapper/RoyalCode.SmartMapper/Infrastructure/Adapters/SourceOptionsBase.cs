using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

// TODO: comentários de SourceOptionsBase
// TODO: mover testes dos métodos que são para AdapterOptions para uma classe de testes desse tipo base

public class SourceOptionsBase : OptionsBase
{
    private ICollection<PropertyOptions>? propertyOptions;

    public SourceOptionsBase(Type sourceType)
    {
        SourceType = sourceType;
    }
    
    public Type SourceType { get; }
    
    /// <summary>
    /// <para>
    ///     Gets or create the options for a property of the source type.
    /// </para>
    /// </summary>
    /// <param name="property">The property of the source type.</param>
    /// <returns>
    ///     The options for the property of the source type or a new instance if no options have been set.
    /// </returns>
    public PropertyOptions GetPropertyOptions(PropertyInfo property)
    {
        // check property type
        if (!property.DeclaringType?.IsAssignableFrom(SourceType) ?? false)
            throw new ArgumentException(
                $"The property {property.Name} is not a property of the source type {SourceType.Name}.");

        var options = propertyOptions?.FirstOrDefault(x => x.Property == property);
        if (options is null)
        {
            options = new PropertyOptions(property);
            propertyOptions ??= new List<PropertyOptions>();
            propertyOptions.Add(options);
        }

        return options;
    }
    
    /// <summary>
    /// <para>
    ///     Gets or create the options for a property of the source type.
    /// </para>
    /// </summary>
    /// <param name="propertyName">The name of the property of the source type.</param>
    /// <param name="options">The options for the property of the source type.</param>
    /// <returns>True if exists a property with the given name, otherwise false.</returns>
    public bool TryGetPropertyOptions(string propertyName, [NotNullWhen(true)] out PropertyOptions? options)
    {
        options = propertyOptions?.FirstOrDefault(x => x.Property.Name == propertyName);
        if (options is null)
        {
            // get source property by name, including inherited type properties
            var propertyInfo = SourceType.GetRuntimeProperty(propertyName);

            //var propertyInfo = SourceType.GetProperty(propertyName);
            if (propertyInfo is not null)
                options = GetPropertyOptions(propertyInfo);
        }

        return options is not null;
    }
}