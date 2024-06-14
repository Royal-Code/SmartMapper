using System.Linq.Expressions;
using RoyalCode.SmartMapper.Mapping.Options;

namespace RoyalCode.SmartMapper.Mapping.Builders.Internal;

/// <inheritdoc />
internal sealed class PropertyThenToBuilder<TProperty, TTargetProperty, TNextProperty>
    : IPropertyThenToBuilder<TProperty, TTargetProperty, TNextProperty>
{
    private readonly ThenToPropertyOptions thenToPropertyOptions;

    /// <summary>
    /// Creates a new instance of <see cref="PropertyThenToBuilder{TProperty, TTargetProperty, TNextProperty}"/>.
    /// </summary>
    /// <param name="thenToPropertyOptions"></param>
    public PropertyThenToBuilder(ThenToPropertyOptions thenToPropertyOptions)
    {
        this.thenToPropertyOptions = thenToPropertyOptions;
    }

    /// <inheritdoc />
    public IPropertyThenToBuilder<TProperty, TTargetProperty, TNextProperty> CastValue()
    {
        var assigmentOptions = thenToPropertyOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseCast();
        return this;
    }

    /// <inheritdoc />
    public IPropertyThenToBuilder<TProperty, TTargetProperty, TNextProperty> UseConverter(
        Expression<Func<TProperty, TNextProperty>> converter)
    {
        var assigmentOptions = thenToPropertyOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseConverter(converter);
        return this;
    }

    /// <inheritdoc />
    public IPropertyThenToBuilder<TProperty, TTargetProperty, TNextProperty> Adapt()
    {
        var assigmentOptions = thenToPropertyOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseAdapt();
        return this;
    }

    /// <inheritdoc />
    public IPropertyThenToBuilder<TProperty, TTargetProperty, TNextProperty> Select()
    {
        var assigmentOptions = thenToPropertyOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseSelect();
        return this;
    }

    /// <inheritdoc />
    public IPropertyThenBuilder<TProperty, TNextProperty> Then()
    {
        return new PropertyThenBuilder<TProperty, TNextProperty>(thenToPropertyOptions);
    }
}