using System.Linq.Expressions;
using RoyalCode.SmartMapper.Configurations.Adapters;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

public class AdapterPropertyToParametersOptionsBuilder<TSource, TSourceProperty>
    : IAdapterPropertyToParametersOptionsBuilder<TSource, TSourceProperty>
{
    private readonly PropertyToConstructorOptions options;
    
    public AdapterPropertyToParametersOptionsBuilder(PropertyToConstructorOptions options)
    {
        this.options = options;
    }

    public IAdapterParameterStrategyBuilder<TSourceProperty, TProperty> Parameter<TProperty>(
        Expression<Func<TSourceProperty, TProperty>> propertySelector, string? parameterName = null)
    {
        throw new NotImplementedException();
    }

    public void Ignore<TProperty>(Expression<Func<TSourceProperty, TProperty>> propertySelector)
    {
        throw new NotImplementedException();
    }
}