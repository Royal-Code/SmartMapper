using System.Reflection;
using RoyalCode.SmartMapper.Extensions;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

public class AdapterSourceToMethodOptions : OptionsBase
{
    private ICollection<PropertyToParameterOptions>? propertyToParameterOptions;

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
}