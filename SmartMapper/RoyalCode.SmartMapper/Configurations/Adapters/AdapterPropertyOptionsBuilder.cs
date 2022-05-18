using RoyalCode.SmartMapper.Extensions;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Configurations.Adapters;

public class AdapterPropertyOptionsBuilder<TSource, TTarget, TProperty>
    : IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty>
{
    private readonly PropertyOptions options;

    public AdapterPropertyOptionsBuilder(PropertyOptions options)
    {
        this.options = options;
    }

    public IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> To<TTargetProperty>(
        Expression<Func<TTarget, TTargetProperty>> propertySelection)
    {
        var targetProperty = propertySelection.GetSelectedProperty();
        options.TargetProperty = targetProperty;
        options.Action = PropertyMapAction.SetValue;

        return new AdapterPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty>(options);
    }

    public IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> To<TTargetProperty>(string propertyName)
    {
        throw new NotImplementedException();
    }
}

public class AdapterPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty>
    : IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty>
{
    private readonly PropertyOptions options;

    public AdapterPropertyOptionsBuilder(PropertyOptions options)
    {
        this.options = options;
    }

    public IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> Adapt()
    {
        options.Action = PropertyMapAction.Adapt;
        return this;
    }

    public IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> UseConverver(
        Expression<Func<TProperty, TTargetProperty>> converter)
    {
        options.Action = PropertyMapAction.Adapt;


        throw new NotImplementedException();
    }
}