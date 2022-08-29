using System.Linq.Expressions;
using System.Reflection;
using RefactorOptions;
using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Extensions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

/// <inheritdoc />
public class AdapterPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty>
    : IAdapterPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty>
{
    private readonly PropertyOptions propertyOptions;
    private readonly ToPropertyOptions toPropertyOptions;

    public AdapterPropertyToPropertyOptionsBuilder(
        PropertyOptions propertyOptions,
        ToPropertyOptions toPropertyOptions)
    {
        this.propertyOptions = propertyOptions;
        this.toPropertyOptions = toPropertyOptions;
    }

    /// <inheritdoc />
    public IAdapterPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> CastValue()
    {
        propertyOptions.GetOrCreateAssignmentStrategyOptions<TProperty>().UseCast();
        return this;
    }

    /// <inheritdoc />
    public IAdapterPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> UseConverter(
        Expression<Func<TProperty, TTargetProperty>> converter)
    {
        propertyOptions.GetOrCreateAssignmentStrategyOptions<TProperty>().UseConvert(converter);
        return this;
    }

    /// <inheritdoc />
    public IAdapterPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> Adapt()
    {
        propertyOptions.GetOrCreateAssignmentStrategyOptions<TProperty>().UseAdapt();
        return this;
    }

    /// <inheritdoc />
    public IAdapterPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> Select()
    {
        propertyOptions.GetOrCreateAssignmentStrategyOptions<TProperty>().UseSelect();
        return this;
    }

    /// <inheritdoc />
    public IAdapterPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> WithService<TService>(
        Expression<Func<TService, TProperty, TTargetProperty>> valueProcessor)
    {
        propertyOptions.GetOrCreateAssignmentStrategyOptions<TProperty>().UseProcessor(valueProcessor);
        return this;
    }

    /// <inheritdoc />
    public IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty> ThenTo<TNextProperty>(
        Expression<Func<TTargetProperty, TNextProperty>> propertySelector)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        var thenToOptions = toPropertyOptions.ThenTo(propertyInfo);

        return new AdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty>(thenToOptions);
    }

    /// <inheritdoc />
    public IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty> Then()
    {
        return new AdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty>(toPropertyOptions);
    }
}