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
    /// Options of the strategy to be used to assign the property value to the destination counterpart.
    /// </summary>
    public AssignmentStrategyOptions? AssignmentStrategy { get; private set; }

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
        UpdateResolutionStatus(ResolutionStatus.MappedToMethodParameter);
        ResolutionOptions = options;
        options.PropertyRelated = this;
    }

    /// <summary>
    /// Sets the mapping of the property to a constructor parameter.
    /// </summary>
    /// <param name="parameterOptions">The options that configure the property to be mapped to a constructor parameter.</param>
    public void MappedToConstructor(ConstructorParameterOptions parameterOptions)
    {
        UpdateResolutionStatus(ResolutionStatus.MappedToConstructor);
        ResolutionOptions = parameterOptions;
        parameterOptions.PropertyRelated = this;
    }
    
    /// <summary>
    /// Sets the mapping of the property to a property of the destination object.
    /// </summary>
    /// <param name="toPropertyOptions">The options that configure the property to be mapped to a property of the destination object.</param>
    public void MappedToProperty(PropertyToPropertyOptions toPropertyOptions)
    {
        UpdateResolutionStatus(ResolutionStatus.MappedToProperty);
        ResolutionOptions = toPropertyOptions;
        toPropertyOptions.PropertyRelated = this;
    }
    
    public void MapInnerProperties(InnerPropertiesOptionsBase options)
    {
        UpdateResolutionStatus(ResolutionStatus.MapInnerProperties);
        ResolutionOptions = options;
        options.PropertyRelated = this;
    }

    /// <summary>
    /// Sets to ignore the mapping of the property.
    /// </summary>
    public void IgnoreMapping()
    {
        UpdateResolutionStatus(ResolutionStatus.Ignored);
    }
    
    /// <summary>
    /// Resets the mapping configuration of the property.
    /// </summary>
    public void ResetMapping()
    {
        ResolutionStatus = ResolutionStatus.Undefined;
        ResolutionOptions = null;
        AssignmentStrategy = null;
    }
    
    /// <summary>
    /// <para>
    ///     Get o create the <see cref="AssignmentStrategyOptions{TProperty}"/>.
    /// </para>
    /// <para>
    ///     When the <see cref="AssignmentStrategy"/> is null or not for the <typeparamref name="TProperty"/> type,
    ///     a new instance of the <see cref="AssignmentStrategyOptions{TProperty}"/> is created.
    /// </para>
    /// </summary>
    /// <typeparam name="TProperty">The type of the source property.</typeparam>
    /// <returns>The <see cref="AssignmentStrategyOptions{TProperty}"/>.</returns>
    public AssignmentStrategyOptions<TProperty> GetOrCreateAssignmentStrategyOptions<TProperty>()
    {
        if (typeof(TProperty) != Property.PropertyType)
            throw new InvalidOperationException($"The type of the property '{Property.Name}' is not '{typeof(TProperty).Name}'");
        
        var strategyOptions = AssignmentStrategy as AssignmentStrategyOptions<TProperty>;
        if (strategyOptions is null)
        {
            strategyOptions = new AssignmentStrategyOptions<TProperty>();
            AssignmentStrategy = strategyOptions;
        }

        return strategyOptions;
    }
    
    private void UpdateResolutionStatus(ResolutionStatus status)
    {
        if (ResolutionStatus == ResolutionStatus.Undefined)
        {
            ResolutionStatus = status;
            return;
        }
        
        if (ResolutionStatus != status)
            throw new InvalidOperationException(
                $"The resolution status of the property '{Property.Name}' is already set to '{ResolutionStatus}'" +
                $" and cannot be changed to '{status}'");
    }
}