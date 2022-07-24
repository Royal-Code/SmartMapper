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
        options.UseCast();
        return this;
    }

    /// <inheritdoc />
    public IAdapterParameterStrategyBuilder<TSource, TProperty> UseConverter<TParameter>(
        Expression<Func<TProperty, TParameter>> converter)
    {
        options.UseConvert(converter);
        return this;
    }

    /// <inheritdoc />
    public IAdapterParameterStrategyBuilder<TSource, TProperty> Adapt()
    {
        options.UseAdapt();
        return this;
    }

    /// <inheritdoc />
    public IAdapterParameterStrategyBuilder<TSource, TProperty> Select()
    {
        options.UseSelect();
        return this;
    }

    /// <inheritdoc />
    public IAdapterParameterStrategyBuilder<TSource, TProperty> WithService<TService, TParameter>(
        Expression<Func<TService, TProperty, TParameter>> valueProcessor)
    {
        options.UseProcessor(valueProcessor);
        return this;
    }
}