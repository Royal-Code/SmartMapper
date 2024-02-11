using System.Linq.Expressions;
using System.Reflection;
using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;
using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

internal sealed class PropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> 
    : IPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> 
{
    private readonly AdapterOptions adapterOptions;
    private readonly ToPropertyResolutionOptions propertyResolutionOptions;

    /// <summary>
    /// Creates a new instance of <see cref="PropertyToPropertyOptionsBuilder{TSource, TTarget, TProperty, TTargetProperty}"/>.
    /// </summary>
    /// <param name="adapterOptions">The adapter options.</param>
    /// <param name="propertyResolutionOptions">The property resolution options.</param>
    public PropertyToPropertyOptionsBuilder(
        AdapterOptions adapterOptions,
        ToPropertyResolutionOptions propertyResolutionOptions)
    {
        this.adapterOptions = adapterOptions;
        this.propertyResolutionOptions = propertyResolutionOptions;
    }
    
    /// <inheritdoc />
    public IPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> CastValue()
    {
        var assigmentOptions = propertyResolutionOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseCast();
        return this;
    }

    /// <inheritdoc />
    public IPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> UseConverter(
        Expression<Func<TProperty, TTargetProperty>> converter)
    {
        var assigmentOptions = propertyResolutionOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseConverter(converter);
        return this;
    }

    /// <inheritdoc />
    public IPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> Adapt()
    {
        var assigmentOptions = propertyResolutionOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseAdapt();
        return this;
    }

    /// <inheritdoc />
    public IPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> Select()
    {
        var assigmentOptions = propertyResolutionOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseSelect();
        return this;
    }

    /// <inheritdoc />
    public IPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty> ThenTo<TNextProperty>(
        Expression<Func<TTargetProperty, TNextProperty>> propertySelector)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));
        
        var thenToPropertyOptions = propertyResolutionOptions.ThenTo(propertyInfo);
        var builder = new PropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty>(thenToPropertyOptions);
        return builder;
    }

    /// <inheritdoc />
    public IPropertyThenOptionsBuilder<TProperty, TTargetProperty> Then()
    {
        throw new NotImplementedException();
    }
}