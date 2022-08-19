using System.Linq.Expressions;
using System.Reflection;
using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Extensions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

public class AdapterPropertyToParametersOptionsBuilder<TSourceProperty>
    : IAdapterPropertyToParametersOptionsBuilder<TSourceProperty>
{
    private readonly SourceOptionsBase sourceOptions;
    private readonly ToParametersTargetRelatedOptionsBase toParametersOptions;

    public AdapterPropertyToParametersOptionsBuilder(
        SourceOptionsBase sourceOptions,
        ToParametersTargetRelatedOptionsBase toParametersOptions)
    {
        this.sourceOptions = sourceOptions;
        this.toParametersOptions = toParametersOptions;
    }

    public IAdapterParameterStrategyBuilder<TProperty> Parameter<TProperty>(
        Expression<Func<TSourceProperty, TProperty>> propertySelector, string? parameterName = null)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        var propertyOptions = sourceOptions.GetPropertyOptions(propertyInfo);
        var constructorParameterOptions = constructorOptions.GetConstructorParameterOptions(propertyInfo);
        propertyOptions.MappedToConstructorParameter(constructorParameterOptions);

        if (parameterName is not null)
            constructorParameterOptions.UseParameterName(parameterName);

        var assigmentOptions = propertyOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        return new AdapterParameterStrategyBuilder<TProperty>(assigmentOptions);
        
        throw new NotImplementedException();
    }

    public void Ignore<TProperty>(Expression<Func<TSourceProperty, TProperty>> propertySelector)
    {
        throw new NotImplementedException();
    }
}