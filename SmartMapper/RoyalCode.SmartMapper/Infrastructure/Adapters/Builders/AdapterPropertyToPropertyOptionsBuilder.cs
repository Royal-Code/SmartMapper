using System.Linq.Expressions;
using RoyalCode.SmartMapper.Configurations.Adapters;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

/// <inheritdoc />
public class AdapterPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty>
    : IAdapterPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty>
{
    private readonly AdapterOptions adapterOptions;
    private readonly PropertyOptions propertyOptions;
    private readonly PropertyToPropertyOptions toPropertyOptions;

    public AdapterPropertyToPropertyOptionsBuilder(AdapterOptions adapterOptions, PropertyOptions propertyOptions,
        PropertyToPropertyOptions toPropertyOptions)
    {
        this.adapterOptions = adapterOptions;
        this.propertyOptions = propertyOptions;
        this.toPropertyOptions = toPropertyOptions;
    }

    /// <inheritdoc />
    public IAdapterPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> CastValue()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IAdapterPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> UseConverter(
        Expression<Func<TProperty, TTargetProperty>> converter)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IAdapterPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> Adapt()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IAdapterPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> Select()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IAdapterPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> WithService<TService>(
        Expression<Func<TService, TProperty, TTargetProperty>> valueProcessor)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty> ThenTo<TNextProperty>(
        Expression<Func<TTargetProperty, TNextProperty>> propertySelection)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty> Then()
    {
        throw new NotImplementedException();
    }
}