using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

public class PropertyToMethodOptions : ToParametersTargetRelatedOptionsBase
{
    private ICollection<PropertyToParameterOptions>? propertyToParameterOptions;
    
    public PropertyToMethodOptions(Type propertyType)
        : base(propertyType)
    { }

    public MethodInfo? Method { get; internal set; }
    
    public string? MethodName { get; internal set; }
    
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
    public override ParameterOptionsBase GetParameterOptions(PropertyInfo property)
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
    public override bool TryGetPropertyToParameterOptions(
        PropertyInfo property, 
        [NotNullWhen(true)] out ParameterOptionsBase? options)
    {
        options = propertyToParameterOptions?.FirstOrDefault(x => x.Property == property);
        return options is not null;
    }
}