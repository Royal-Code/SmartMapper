using System.Linq.Expressions;
using RoyalCode.SmartMapper.Configurations.Adapters;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

/// <inheritdoc />
public class AdapterPropertyToMethodOptionsBuilder<TTarget, TProperty>
    : IAdapterPropertyToMethodOptionsBuilder<TTarget, TProperty>
{
    private readonly PropertyToMethodOptions options;

    public AdapterPropertyToMethodOptionsBuilder(PropertyToMethodOptions options)
    {
        this.options = options;
    }

    /// <inheritdoc />
    public void Parameters(Action<IAdapterPropertyToParametersOptionsBuilder<TProperty>> configureParameters)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Value(Action<IAdapterParameterStrategyBuilder<TProperty>> configureProperty)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IAdapterPropertyToMethodOptionsBuilder<TTarget, TProperty> UseMethod(string name)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IAdapterPropertyToMethodOptionsBuilder<TTarget, TProperty> UseMethod(Expression<Func<TTarget, Delegate>> methodSelector)
    {
        throw new NotImplementedException();
    }
}