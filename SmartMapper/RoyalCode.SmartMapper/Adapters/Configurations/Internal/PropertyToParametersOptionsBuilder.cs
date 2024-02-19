using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;
using System.Linq.Expressions;
using System.Reflection;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

internal abstract class PropertyToParametersOptionsBuilderBase<TSourceProperty> 
    : IPropertyToParametersOptionsBuilder<TSourceProperty>
{
    protected readonly SourceOptions sourceOptions;

    /// <summary>
    /// Creates a new base builder to map properties to parameters.
    /// </summary>
    /// <param name="sourceOptions">The source options</param>
    protected PropertyToParametersOptionsBuilderBase(SourceOptions sourceOptions)
    {
        this.sourceOptions = sourceOptions;
    }

    public void Ignore<TProperty>(Expression<Func<TSourceProperty, TProperty>> propertySelector)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        var propertyOptions = sourceOptions.GetPropertyOptions(propertyInfo);

        propertyOptions.IgnoreMapping();
    }

    public abstract IParameterStrategyBuilder<TProperty> Parameter<TProperty>(
        Expression<Func<TSourceProperty, TProperty>> propertySelector, 
        string? parameterName = null);
}

internal sealed class PropertyToParametersOptionsBuilder<TSourceProperty>
    : PropertyToParametersOptionsBuilderBase<TSourceProperty>
{
    private readonly InnerPropertiesOptions innerPropertiesOptions;

    public PropertyToParametersOptionsBuilder(InnerPropertiesOptions innerPropertiesOptions) 
        : base(innerPropertiesOptions.InnerSourceOptions)
    {
        this.innerPropertiesOptions = innerPropertiesOptions;
    }

    public override IParameterStrategyBuilder<TProperty> Parameter<TProperty>(
        Expression<Func<TSourceProperty, TProperty>> propertySelector,
        string? parameterName = null)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        

        throw new NotImplementedException();
    }
}