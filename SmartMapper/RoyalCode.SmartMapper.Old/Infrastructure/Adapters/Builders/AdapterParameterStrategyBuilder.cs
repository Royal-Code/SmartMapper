using System.Linq.Expressions;
using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

/// <inheritdoc />
public class AdapterParameterStrategyBuilder<TProperty> : IAdapterParameterStrategyBuilder<TProperty>
{
    private readonly AssignmentStrategyOptions<TProperty> options;

    public AdapterParameterStrategyBuilder(AssignmentStrategyOptions<TProperty> options)
    {
        this.options = options;
    }

    /// <inheritdoc />
    public IAdapterParameterStrategyBuilder<TProperty> CastValue()
    {
        options.UseCast();
        return this;
    }

    /// <inheritdoc />
    public IAdapterParameterStrategyBuilder<TProperty> UseConverter<TParameter>(
        Expression<Func<TProperty, TParameter>> converter)
    {
        options.UseConvert(converter);
        return this;
    }

    /// <inheritdoc />
    public IAdapterParameterStrategyBuilder<TProperty> Adapt()
    {
        options.UseAdapt();
        return this;
    }

    /// <inheritdoc />
    public IAdapterParameterStrategyBuilder<TProperty> Select()
    {
        options.UseSelect();
        return this;
    }

    /// <inheritdoc />
    public IAdapterParameterStrategyBuilder<TProperty> WithService<TService, TParameter>(
        Expression<Func<TService, TProperty, TParameter>> valueProcessor)
    {
        options.UseProcessor(valueProcessor);
        return this;
    }
}