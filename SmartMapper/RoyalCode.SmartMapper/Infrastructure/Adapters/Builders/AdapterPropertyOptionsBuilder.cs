using System.Linq.Expressions;
using RoyalCode.SmartMapper.Configurations.Adapters;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

public class AdapterPropertyOptionsBuilder<TSource, TTarget, TProperty> 
    : IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty>
{
    private readonly AdapterOptions adapterOptions;
    private readonly PropertyOptions propertyOptions;

    public AdapterPropertyOptionsBuilder(AdapterOptions adapterOptions, PropertyOptions propertyOptions)
    {
        this.adapterOptions = adapterOptions;
        this.propertyOptions = propertyOptions;
    }

    public IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> To<TTargetProperty>(
        Expression<Func<TTarget, TTargetProperty>> propertySelection)
    {
        throw new NotImplementedException();
    }

    public IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> To<TTargetProperty>(
        string propertyName)
    {
        throw new NotImplementedException();
    }

    public IAdapterPropertyToConstructorOptionsBuilder<TSource, TProperty> ToConstructor()
    {
        throw new NotImplementedException();
    }

    public IAdapterPropertyToMethodOptionsBuilder<TSource, TTarget, TProperty> ToMethod()
    {
        throw new NotImplementedException();
    }

    public IAdapterPropertyToMethodOptionsBuilder<TSource, TTarget, TProperty> ToMethod(
        Expression<Func<TTarget, Delegate>> methodSelect)
    {
        throw new NotImplementedException();
    }
}