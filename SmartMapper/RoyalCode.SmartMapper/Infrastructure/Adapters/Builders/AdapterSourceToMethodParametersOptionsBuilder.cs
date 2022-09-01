using System.Linq.Expressions;
using System.Reflection;
using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Extensions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

/// <inheritdoc />
public class AdapterSourceToMethodParametersOptionsBuilder<TSource> 
    : IAdapterSourceToMethodParametersOptionsBuilder<TSource>
{
    private readonly AdapterOptions adapterOptions;
    private readonly SourceToMethodOptions methodOptions;

    public AdapterSourceToMethodParametersOptionsBuilder(
        AdapterOptions adapterOptions,
        SourceToMethodOptions methodOptions)
    {
        this.adapterOptions = adapterOptions;
        this.methodOptions = methodOptions;
    }

    /// <inheritdoc />
    public IAdapterParameterStrategyBuilder<TProperty> Parameter<TProperty>(
        Expression<Func<TSource, TProperty>> propertySelector)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));
        
        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));
        
        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(propertyInfo);
        
        methodOptions.AddPropertyToParameterSequence(propertyOptions);

        var strategyOptions = propertyOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        return new AdapterParameterStrategyBuilder<TProperty>(strategyOptions);
    }
}