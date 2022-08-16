using System.Linq.Expressions;
using RoyalCode.SmartMapper.Configurations.Adapters;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

public class AdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty>
    : IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty>
{
    private readonly ThenToPropertyOptions options;

    public AdapterPropertyThenOptionsBuilder(ThenToPropertyOptions options)
    {
        this.options = options;
    }

    public IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty> CastValue()
    {
        throw new NotImplementedException();
    }

    public IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty> UseConverter(
        Expression<Func<TProperty, TTargetProperty>> converter)
    {
        throw new NotImplementedException();
    }

    public IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty> Adapt()
    {
        throw new NotImplementedException();
    }

    public IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty> Select()
    {
        throw new NotImplementedException();
    }

    public IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty> WithService<TService>(
        Expression<Func<TService, TProperty, TTargetProperty>> valueProcessor)
    {
        throw new NotImplementedException();
    }

    public IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty> Then()
    {
        throw new NotImplementedException();
    }
}

public class AdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty>
    : IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty>
{
    private readonly PropertyToPropertyOptions toPropertyOptions;

    public AdapterPropertyThenOptionsBuilder(PropertyToPropertyOptions toPropertyOptions)
    {
        this.toPropertyOptions = toPropertyOptions;
    }

    public IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty> To<TNextProperty>(
        Expression<Func<TTargetProperty, TNextProperty>> propertySelection)
    {
        throw new NotImplementedException();
    }

    public IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty> To<TNextProperty>(
        string propertyName)
    {
        throw new NotImplementedException();
    }

    public IAdapterPropertyToMethodOptionsBuilder<TProperty, TTargetProperty, TProperty> ToMethod()
    {
        throw new NotImplementedException();
    }

    public IAdapterPropertyToMethodOptionsBuilder<TProperty, TTargetProperty, TProperty> ToMethod(
        Expression<Func<TTargetProperty, Delegate>> methodSelect)
    {
        throw new NotImplementedException();
    }
}