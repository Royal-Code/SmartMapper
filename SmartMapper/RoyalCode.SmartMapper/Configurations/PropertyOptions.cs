using System.Reflection;
using RoyalCode.Extensions.PropertySelection;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Configurations;

public class PropertyOptions : OptionsBase
{
    public PropertyOptions(PropertyInfo sourceProperty)
    {
        SourceProperty = sourceProperty;
    }
    
    public PropertyInfo SourceProperty { get; }
    
    public PropertySelection? TargetProperty { get; internal set; }
    
    public PropertyToMethodOptions? TargetMethodOptions { get; internal set; }
    
    public PropertyMapAction Action { get; internal set; }
}

public class PropertyToMethodOptions
{
    private readonly Dictionary<PropertyInfo, PropertyToParameterOptions> paramtersOptions = new();

    public PropertyToMethodOptions(PropertyOptions propertyOptions)
    {
        PropertyOptions = propertyOptions;
    }

    public PropertyOptions PropertyOptions { get; }

    public PropertyInfo SourceProperty => PropertyOptions.SourceProperty;
    
    public MethodInfo? TargetMethod { get; internal set; }
    
    public string MethodName { get; internal set; }

    public PropertyToParameterOptions GetParameterOptions(PropertyInfo property)
    {
        if (paramtersOptions.TryGetValue(property, out var parameter))
            return parameter;

        parameter = new PropertyToParameterOptions(this, property);
        paramtersOptions[property] = parameter;
        return parameter;
    }
}

public class PropertyToParameterOptions
{
    public PropertyToParameterOptions(PropertyToMethodOptions propertyToMethodOptions, PropertyInfo propertyToParameterInfo)
    {
        PropertyToMethodOptions = propertyToMethodOptions;
        PropertyToParameterInfo = propertyToParameterInfo;
    }

    public PropertyToMethodOptions PropertyToMethodOptions { get; }
    
    public PropertyInfo PropertyToParameterInfo { get; }
    
    public ParameterInfo? ParameterInfo { get; internal set; }

    public MapAction MapAction { get; } = new();
}