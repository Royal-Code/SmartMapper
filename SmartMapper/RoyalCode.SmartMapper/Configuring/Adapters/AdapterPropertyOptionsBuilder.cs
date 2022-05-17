using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Configuring.Adapters;

public class AdapterPropertyOptionsBuilder<TSource, TTarget, TProperty>
    : IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty>
{
    public IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> To<TTargetProperty>(
        Expression<Func<TTarget, TTargetProperty>> propertySelection)
    {
        
        
        throw new NotImplementedException();
    }

    public IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> To<TTargetProperty>(string propertyName)
    {
        throw new NotImplementedException();
    }
}