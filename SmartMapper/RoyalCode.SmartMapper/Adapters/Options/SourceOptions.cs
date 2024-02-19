using System.Linq.Expressions;
using System.Reflection;
using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;

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
    ///     Gets or create the options for a property of the source type from a lambda expression.
    /// </para>
    /// </summary>
    /// <param name="propertySelector">
    ///     A lambda expression that get a property value, used to extract the property info.
    /// </param>
    /// <returns>
    ///     The options for the property of the source type or a new instance if no options have been set.
    /// </returns>
    /// <exception cref="InvalidPropertySelectorException">
    ///     If the lambda expression does not select a property.
    /// </exception>
    public PropertyOptions GetPropertyOptions(LambdaExpression propertySelector)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        return GetPropertyOptions(propertyInfo);
    }

    /// <summary>
    /// <para>
    ///     Gets or create the options for a property of the source type from a lambda expression.
    /// </para>
    /// </summary>
    /// <param name="propertyName">The property name.</param>
    /// <param name="propertyType">The property type.</param>
    /// <returns></returns>
    /// <exception cref="InvalidPropertyNameException">
    ///     If the source type does not contains the a property with informed name.
    /// </exception>
    /// <exception cref="InvalidPropertyTypeException">
    ///     If the source property is not of the informed type.
    /// </exception>
    public PropertyOptions GetPropertyOptions(string propertyName, Type propertyType)
    {
        var options = propertyOptions?.FirstOrDefault(x => x.Property.Name == propertyName);
        if (options is not null)
            return options;

        // get target property by name, including inherited type properties
        var propertyInfo = SourceType.GetRuntimeProperty(propertyName);

        // check if property exists
        if (propertyInfo is null)
            throw new InvalidPropertyNameException(
                $"The type '{SourceType.Name}' does not have a property with name '{propertyName}'.",
                nameof(propertyName));

        // validate the property type
        if (propertyInfo.PropertyType != propertyType)
            throw new InvalidPropertyTypeException(
                $"Property '{propertyName}' on type '{SourceType.Name}' " +
                $"is not of type '{propertyType.Name}', " +
                $"but of type '{propertyInfo.PropertyType.Name}'.",
                nameof(propertyName));

        return GetPropertyOptions(propertyInfo);
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