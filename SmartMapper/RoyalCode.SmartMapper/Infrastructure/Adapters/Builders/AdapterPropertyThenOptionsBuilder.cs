using System.Linq.Expressions;
using RoyalCode.SmartMapper.Configurations.Adapters;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

public class AdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty>
    : IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty>
{
    
    
    public IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty> CastValue()
    {
        throw new NotImplementedException();
    }

    public IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty> UseConverter(Expression<Func<TProperty, TTargetProperty>> converter)
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

    public IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty> WithService<TService>(Expression<Func<TService, TProperty, TTargetProperty>> valueProcessor)
    {
        throw new NotImplementedException();
    }

    public IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty> Then()
    {
        throw new NotImplementedException();
    }
}