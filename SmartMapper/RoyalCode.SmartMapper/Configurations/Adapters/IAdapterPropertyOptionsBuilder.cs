
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Configurations.Adapters;

public interface IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty>
{
    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty> To<TTargetProperty>(
        Expression<Func<TTarget, TTargetProperty>> propertySelection);

    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty> To<TTargetProperty>(string propertyName);

    IConstructorPropertyOptionsBuilder<TSource, TSourceProperty> ToConstructor();

    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty> ToConstructorParameter();

    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty> ToMethod();

    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty> ToMethodParameter();
}

public interface IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty>
{
    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty> Adapt();

    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty> UseConverver(
        Expression<Func<TSourceProperty, TTargetProperty>> converter);
    
    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TNextProperty> ThenTo<TNextProperty>(
        Expression<Func<TTargetProperty, TNextProperty>> propertySelection);
}

public interface IConstructorPropertyOptionsBuilder<TSource, TSourceProperty>
{
    IConstructorParameterPropertyOptionsBuilder<TSource, TSourceProperty, TProperty> Map<TProperty>(
        Expression<Func<TSourceProperty, TProperty>> propertySelection);
}

public interface IConstructorParameterPropertyOptionsBuilder<TSource, TSourceProperty, TParameterProperty>
{
    
}
