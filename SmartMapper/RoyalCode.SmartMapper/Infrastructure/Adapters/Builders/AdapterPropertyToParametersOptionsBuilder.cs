using System.Linq.Expressions;
using System.Reflection;
using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Extensions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

/// <inheritdoc />
public abstract class AdapterPropertyToParametersOptionsBuilder<TSourceProperty, TToParameter>
    : IAdapterPropertyToParametersOptionsBuilder<TSourceProperty>
    where TToParameter : ToParameterOptionsBase
{
    private readonly SourceOptions sourceOptions;
    private readonly ParametersOptionsBase<TToParameter> parametersOptions;

    protected AdapterPropertyToParametersOptionsBuilder(
        SourceOptions sourceOptions,
        ParametersOptionsBase<TToParameter> parametersOptions)
    {
        this.sourceOptions = sourceOptions;
        this.parametersOptions = parametersOptions;
    }

    /// <inheritdoc />
    public IAdapterParameterStrategyBuilder<TProperty> Parameter<TProperty>(
        Expression<Func<TSourceProperty, TProperty>> propertySelector, string? parameterName = null)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        var propertyOptions = sourceOptions.GetPropertyOptions(propertyInfo);
        var parameterOptions = parametersOptions.GetParameterOptions(propertyInfo);

        Map(propertyOptions, parameterOptions);

        if (parameterName is not null)
            parameterOptions.UseParameterName(parameterName);

        var assigmentOptions = propertyOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        return new AdapterParameterStrategyBuilder<TProperty>(assigmentOptions);
    }

    /// <inheritdoc />
    public void Ignore<TProperty>(Expression<Func<TSourceProperty, TProperty>> propertySelector)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));
        
        var propertyOptions = sourceOptions.GetPropertyOptions(propertyInfo);
        propertyOptions.IgnoreMapping();
    }

    /// <summary>
    /// Apply the mapping of the parameter to the source property.
    /// </summary>
    /// <param name="propertyOptions">The source property.</param>
    /// <param name="parameterOptions">The target parameter.</param>
    protected abstract void Map(PropertyOptions propertyOptions, TToParameter parameterOptions);
}