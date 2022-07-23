using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

/// <summary>
/// <para>
///     Options for one property of the source object.
/// </para>
/// <para>
///     Contains the resolution for the mapping of the property.
/// </para>
/// </summary>
public class PropertyOptions : OptionsBase
{
    public PropertyOptions(PropertyInfo property)
    {
        Property = property;
    }

    public PropertyInfo Property { get; }
    
    public ResolutionStatus ResolutionStatus { get; private set; }
    
    public OptionsBase? ResolutionOptions { get; private set; }
    
    public void MappedToMethodParameter(PropertyToParameterOptions options)
    {
        ResolutionStatus = ResolutionStatus.MappedToMethodParameter;
        ResolutionOptions = options;
        options.PropertyRelated = this;
    }
    
    public void IgnoreMapping()
    {
        ResolutionStatus = ResolutionStatus.Ignore;
        ResolutionOptions = null;
    }
    
    public void ResetMapping()
    {
        ResolutionStatus = ResolutionStatus.Undefined;
        ResolutionOptions = null;
    }
}