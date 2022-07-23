using System.Linq.Expressions;
using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Extensions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

/// <inheritdoc />
public class AdapterSourceToMethodParametersOptionsBuilder<TSource> 
    : IAdapterSourceToMethodParametersOptionsBuilder<TSource>
{
    private readonly AdapterOptions adapterOptions;
    private readonly AdapterSourceToMethodOptions methodOptions;

    public AdapterSourceToMethodParametersOptionsBuilder(
        AdapterOptions adapterOptions,
        AdapterSourceToMethodOptions methodOptions)
    {
        this.adapterOptions = adapterOptions;
        this.methodOptions = methodOptions;
    }

    /// <inheritdoc />
    public IAdapterParameterStrategyBuilder<TSource, TProperty> Parameter<TProperty>(
        Expression<Func<TSource, TProperty>> propertySelector)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new ArgumentException("Invalid property selector.");
        
        throw new NotImplementedException();
    }
}