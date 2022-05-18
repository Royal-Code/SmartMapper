
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Configurations.Adapters;

public interface IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty>
{
    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty> To<TTargetProperty>(Expression<Func<TTarget, TTargetProperty>> propertySelection);

    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty> To<TTargetProperty>(string propertyName);
}

public interface IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty>
{
    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty> Adapt();

    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty> UseConverver(
        Expression<Func<TSourceProperty, TTargetProperty>> converter);
}