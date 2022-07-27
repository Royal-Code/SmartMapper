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
    /// <summary>
    ///     Creates a new instance of the <see cref="PropertyOptions" /> class.
    /// </summary>
    /// <param name="property">The property to map.</param>
    public PropertyOptions(PropertyInfo property)
    {
        Property = property;
    }

    /// <summary>
    /// The property of the source object.
    /// </summary>
    public PropertyInfo Property { get; }
    
    /// <summary>
    /// The kind or status of the mapping of the property.
    /// </summary>
    public ResolutionStatus ResolutionStatus { get; private set; }
    
    /// <summary>
    /// <para>
    ///     Represents an options for the mapping of the property.
    /// </para>
    /// <para>
    ///     Contains an options related to the resolution of the property.
    /// </para>
    /// </summary>
    public OptionsBase? ResolutionOptions { get; private set; }
    
    /// <summary>
    /// Sets the mapping of the property to be an method parameter.
    /// </summary>
    /// <param name="options">The options that configure the property to be mapped to a method parameter.</param>
    public void MappedToMethodParameter(PropertyToParameterOptions options)
    {
        ResolutionStatus = ResolutionStatus.MappedToMethodParameter;
        ResolutionOptions = options;
        options.PropertyRelated = this;
    }
    
    /// <summary>
    /// Sets to ignore the mapping of the property.
    /// </summary>
    public void IgnoreMapping()
    {
        ResolutionStatus = ResolutionStatus.Ignore;
        ResolutionOptions = null;
    }
    
    /// <summary>
    /// Resets the mapping configuration of the property.
    /// </summary>
    public void ResetMapping()
    {
        ResolutionStatus = ResolutionStatus.Undefined;
        ResolutionOptions = null;
    }
}