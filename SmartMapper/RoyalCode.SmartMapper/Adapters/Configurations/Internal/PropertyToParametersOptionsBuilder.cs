using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;
using System.Linq.Expressions;
using System.Reflection;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

internal abstract class PropertyToParametersOptionsBuilderBase<TSourceProperty> 
    : IPropertyToParametersOptionsBuilder<TSourceProperty>
{
    protected readonly AdapterOptions adapterOptions;

    /// <summary>
    /// Creates a new base builder to map properties to parameters.
    /// </summary>
    /// <param name="adapterOptions"></param>
    protected PropertyToParametersOptionsBuilderBase(AdapterOptions adapterOptions)
    {
        this.adapterOptions = adapterOptions;
    }

    public void Ignore<TProperty>(Expression<Func<TSourceProperty, TProperty>> propertySelector)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(propertyInfo);

        propertyOptions.IgnoreMapping();
    }

    public abstract IParameterStrategyBuilder<TProperty> Parameter<TProperty>(
        Expression<Func<TSourceProperty, TProperty>> propertySelector, 
        string? parameterName = null);
}

internal sealed class PropertyToParametersOptionsBuilder<TSourceProperty>
    : PropertyToParametersOptionsBuilderBase<TSourceProperty>
{
    public PropertyToParametersOptionsBuilder(AdapterOptions adapterOptions) 
        : base(adapterOptions)
    {
    }

    public override IParameterStrategyBuilder<TProperty> Parameter<TProperty>(
        Expression<Func<TSourceProperty, TProperty>> propertySelector,
        string? parameterName = null)
    {
        throw new NotImplementedException();
    }
}