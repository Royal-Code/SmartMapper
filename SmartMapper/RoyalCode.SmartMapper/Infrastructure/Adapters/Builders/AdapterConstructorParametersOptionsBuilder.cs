using System.Linq.Expressions;
using System.Reflection;
using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Extensions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

/// <inheritdoc />
public class AdapterConstructorParametersOptionsBuilder<TSource> : IAdapterConstructorParametersOptionsBuilder<TSource>
{
    private readonly AdapterOptions adapterOptions;
    private readonly ConstructorOptions constructorOptions;

    public AdapterConstructorParametersOptionsBuilder(
        AdapterOptions adapterOptions,
        ConstructorOptions constructorOptions)
    {
        this.adapterOptions = adapterOptions;
        this.constructorOptions = constructorOptions;
    }
    
    /// <inheritdoc />
    public IAdapterParameterStrategyBuilder<TProperty> Parameter<TProperty>(
        Expression<Func<TSource, TProperty>> propertySelector,
        string? parameterName = null)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(propertyInfo);
        var constructorParameterOptions = constructorOptions.GetParameterOptions(propertyInfo);
        
        propertyOptions.MappedToConstructorParameter(constructorParameterOptions);

        if (parameterName is not null)
            constructorParameterOptions.UseParameterName(parameterName);

        var assigmentOptions = propertyOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        return new AdapterParameterStrategyBuilder<TProperty>(assigmentOptions);
    }
}