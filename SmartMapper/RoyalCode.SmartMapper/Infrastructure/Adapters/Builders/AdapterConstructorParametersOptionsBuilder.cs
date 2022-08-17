using System.Linq.Expressions;
using System.Reflection;
using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Extensions;

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

        var propertyOptions = adapterOptions.GetPropertyOptions(propertyInfo);
        var constructorParameterOptions = constructorOptions.GetConstructorParameterOptions(propertyInfo);
        propertyOptions.MappedToConstructor(constructorParameterOptions);

        if (parameterName is not null)
            constructorParameterOptions.UseParameterName(parameterName);

        var assigmentOptions = propertyOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        return new AdapterParameterStrategyBuilder<TProperty>(assigmentOptions);
    }
}