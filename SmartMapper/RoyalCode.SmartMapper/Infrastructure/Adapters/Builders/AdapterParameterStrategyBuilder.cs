using System.Linq.Expressions;
using RoyalCode.SmartMapper.Configurations.Adapters;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

/// <inheritdoc />
public class AdapterParameterStrategyBuilder<TSource, TProperty> : IAdapterParameterStrategyBuilder<TSource, TProperty>
{
    private readonly AssignmentStrategyOptions options;

    public AdapterParameterStrategyBuilder(AssignmentStrategyOptions options)
    {
        this.options = options;
    }

    /// <inheritdoc />
    public IAdapterParameterStrategyBuilder<TSource, TProperty> CastValue()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IAdapterParameterStrategyBuilder<TSource, TProperty> UseConverter<TParameter>(Expression<Func<TProperty, TParameter>> converter)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IAdapterParameterStrategyBuilder<TSource, TProperty> Adapt()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IAdapterParameterStrategyBuilder<TSource, TProperty> Select()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IAdapterParameterStrategyBuilder<TSource, TProperty> WithService<TService, TParameter>(Expression<Func<TService, TProperty, TParameter>> valueProcessor)
    {
        throw new NotImplementedException();
    }
}