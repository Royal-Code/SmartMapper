using System.Linq.Expressions;
using RoyalCode.SmartMapper.Adapters.Options;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

/// <inheritdoc />
internal sealed class PropertyThenToOptionsBuilder<TProperty, TTargetProperty, TNextProperty> 
    : IPropertyThenToOptionsBuilder<TProperty, TTargetProperty, TNextProperty>
{
    private readonly ThenToPropertyOptions thenToPropertyOptions;

    /// <summary>
    /// Creates a new instance of <see cref="PropertyThenToOptionsBuilder{TProperty, TTargetProperty, TNextProperty}"/>.
    /// </summary>
    /// <param name="thenToPropertyOptions"></param>
    public PropertyThenToOptionsBuilder(ThenToPropertyOptions thenToPropertyOptions)
    {
        this.thenToPropertyOptions = thenToPropertyOptions;
    }

    /// <inheritdoc />
    public IPropertyThenToOptionsBuilder<TProperty, TTargetProperty, TNextProperty> CastValue()
    {
        var assigmentOptions = thenToPropertyOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseCast();
        return this;
    }

    /// <inheritdoc />
    public IPropertyThenToOptionsBuilder<TProperty, TTargetProperty, TNextProperty> UseConverter(
        Expression<Func<TProperty, TNextProperty>> converter)
    {
        var assigmentOptions = thenToPropertyOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseConverter(converter);
        return this;
    }

    /// <inheritdoc />
    public IPropertyThenToOptionsBuilder<TProperty, TTargetProperty, TNextProperty> Adapt()
    {
        var assigmentOptions = thenToPropertyOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseAdapt();
        return this;
    }

    /// <inheritdoc />
    public IPropertyThenToOptionsBuilder<TProperty, TTargetProperty, TNextProperty> Select()
    {
        var assigmentOptions = thenToPropertyOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseSelect();
        return this;
    }

    /// <inheritdoc />
    public IPropertyThenOptionsBuilder<TProperty, TNextProperty> Then()
    {
        return new PropertyThenOptionsBuilder<TProperty, TNextProperty>(thenToPropertyOptions);
    }
}