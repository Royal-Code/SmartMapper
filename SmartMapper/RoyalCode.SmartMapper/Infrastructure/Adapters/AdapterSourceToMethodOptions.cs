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
    
    public void ClearParameters()
    {
        propertyToParameterOptions?.ForEach(p => p.Reset());
        propertyToParameterOptions = null;
    }
    
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