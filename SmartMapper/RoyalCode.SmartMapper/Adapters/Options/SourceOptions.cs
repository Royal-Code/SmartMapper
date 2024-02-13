using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using RoyalCode.SmartMapper.Core.Exceptions;

namespace RoyalCode.SmartMapper.Adapters.Options;

/// <summary>
/// Contains options configured for mapping of a source type.
/// </summary>
public sealed class SourceOptions
{
    private ICollection<PropertyOptions>? propertyOptions;
    private ICollection<SourceToMethodOptions>? sourceToMethodOptions;
    
    /// <summary>
    /// Creates a new instance of <see cref="SourceOptions"/>.
    /// </summary>
    /// <param name="sourceType">The source type to configure.</param>
    public SourceOptions(Type sourceType)
    {
        SourceType = sourceType;
    }

    /// <summary>
    /// The source type to configure.
    /// </summary>
    public Type SourceType { get;  }
    
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
    /// <returns>The options for the property of the source type or a new instance if no options have been set.</returns>
    public PropertyOptions GetPropertyOptions(string propertyName)
    {
        var options = propertyOptions?.FirstOrDefault(x => x.Property.Name == propertyName);
        if (options is null)
        {
            // get source property by name, including inherited type properties
            var propertyInfo = SourceType.GetRuntimeProperty(propertyName);

            if (propertyInfo is not null)
                options = GetPropertyOptions(propertyInfo);
        }

        if (options is null)
            throw new InvalidPropertyNameException(
                $"Property '{propertyName}' not found on type '{SourceType.Name}'.", nameof(propertyName));
        
        return options;
    }

    /// <summary>
    /// Gets all options of source to method.
    /// </summary>
    /// <returns>
    ///     All configured options of source to method or empty.
    /// </returns>
    public IEnumerable<SourceToMethodOptions> GetSourceToMethodOptions()
        => sourceToMethodOptions ?? Enumerable.Empty<SourceToMethodOptions>();

    /// <summary>
    /// <para>
    ///     Create a source to method options for the given method options.
    /// </para>
    /// </summary>
    /// <param name="methodOptions">The method options.</param>
    /// <returns>A new instance of the <see cref="SourceToMethodOptions"/> class.</returns>
    public SourceToMethodOptions CreateSourceToMethodOptions(MethodOptions methodOptions)
    {
        var options = new SourceToMethodOptions(methodOptions);
        sourceToMethodOptions ??= [];
        sourceToMethodOptions.Add(options);
        return options;
    }
}