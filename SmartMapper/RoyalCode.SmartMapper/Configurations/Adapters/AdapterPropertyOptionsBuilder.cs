using RoyalCode.SmartMapper.Extensions;
using System.Linq.Expressions;
using RoyalCode.Extensions.PropertySelection;

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
        options.TargetProperty = new PropertySelection(targetProperty);

        return new AdapterPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty>(options);
    }

    public IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> To<TTargetProperty>(string propertyName)
    {
        throw new NotImplementedException();
    }

    public IAdapterPropertyToConstructorOptionsBuilder<TSource, TProperty> ToConstructor()
    {
        throw new NotImplementedException();
    }

    public IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty> ToConstructorParameter()
    {
        throw new NotImplementedException();
    }

    public IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty> ToMethod()
    {
        throw new NotImplementedException();
    }

    public IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty> ToMethodParameter()
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

    public IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> UseConverter(
        Expression<Func<TProperty, TTargetProperty>> converter)
    {
        options.Action = PropertyMapAction.Adapt;


        throw new NotImplementedException();
    }

    public IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty, TNextProperty> ThenTo<TNextProperty>(
        Expression<Func<TTargetProperty, TNextProperty>> propertySelection)
    {
        if (options.TargetProperty is null)
            throw new InvalidOperationException("Can't select next property when the current selection is null");
        
        var targetProperty = propertySelection.GetSelectedProperty();
        options.TargetProperty = options.TargetProperty.SelectChild(targetProperty);

        return new AdapterPropertyOptionsBuilder<TSource, TTarget, TProperty, TNextProperty>(options);
    }
}