using System.Linq.Expressions;
using RoyalCode.SmartMapper.Configurations.Adapters;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

/// <inheritdoc />
public class AdapterPropertyToMethodOptionsBuilder<TSource, TTarget, TProperty>
    : IAdapterPropertyToMethodOptionsBuilder<TSource, TTarget, TProperty>
{
    private readonly PropertyToMethodOptions options;

    public AdapterPropertyToMethodOptionsBuilder(PropertyToMethodOptions options)
    {
        this.options = options;
    }

    /// <inheritdoc />
    public void Parameters(Action<IAdapterPropertyToParametersOptionsBuilder<TSource, TProperty>> configureParameters)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Value(Action<IAdapterParameterStrategyBuilder<TSource, TProperty>> configureProperty)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IAdapterPropertyToMethodOptionsBuilder<TSource, TTarget, TProperty> UseMethod(string name)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IAdapterPropertyToMethodOptionsBuilder<TSource, TTarget, TProperty> UseMethod(Expression<Func<TTarget, Delegate>> methodSelector)
    {
        throw new NotImplementedException();
    }
}