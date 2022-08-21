using System.Linq.Expressions;
using System.Reflection;
using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Extensions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

/// <inheritdoc />
public class AdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty>
    : IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty>
{
    private readonly ToPropertyTargetRelatedOptionsBase options;

    public AdapterPropertyThenOptionsBuilder(ToPropertyTargetRelatedOptionsBase options)
    {
        this.options = options;
    }

    private AssignmentStrategyOptions<TProperty> AssignmentStrategyOptions
        => options.PropertyRelated?.GetOrCreateAssignmentStrategyOptions<TProperty>()
           ?? throw new InvalidOperationException("Property related options not found");

    /// <inheritdoc />
    public IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty> CastValue()
    {
        AssignmentStrategyOptions.UseCast();
        return this;
    }

    /// <inheritdoc />
    public IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty> UseConverter(
        Expression<Func<TProperty, TNextProperty>> converter)
    {
        AssignmentStrategyOptions.UseConvert(converter);
        return this;
    }

    /// <inheritdoc />
    public IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty> Adapt()
    {
        AssignmentStrategyOptions.UseAdapt();
        return this;
    }

    /// <inheritdoc />
    public IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty> Select()
    {
        AssignmentStrategyOptions.UseSelect();
        return this;
    }

    /// <inheritdoc />
    public IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty> WithService<TService>(
        Expression<Func<TService, TProperty, TNextProperty>> valueProcessor)
    {
        AssignmentStrategyOptions.UseProcessor(valueProcessor);
        return this;
    }

    /// <inheritdoc />
    public IAdapterPropertyThenOptionsBuilder<TProperty, TNextProperty> Then()
    {
        return new AdapterPropertyThenOptionsBuilder<TProperty, TNextProperty>(options);
    }
}

/// <inheritdoc />
public class AdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty>
    : IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty>
{
    private readonly ToPropertyTargetRelatedOptionsBase options;

    public AdapterPropertyThenOptionsBuilder(ToPropertyTargetRelatedOptionsBase options)
    {
        this.options = options;
    }

    /// <inheritdoc />
    public IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty> To<TNextProperty>(
        Expression<Func<TTargetProperty, TNextProperty>> propertySelector)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        var thenToOptions = options.ThenTo(propertyInfo);

        return new AdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty>(thenToOptions);
    }

    /// <inheritdoc />
    public IAdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty> To<TNextProperty>(
        string propertyName)
    {
        // get target property by name, including inherited type properties
        var propertyInfo = typeof(TTargetProperty).GetRuntimeProperty(propertyName);
        
        if (propertyInfo is null)
            throw new InvalidPropertyNameException(
                $"Property '{propertyName}' not found on type '{typeof(TTargetProperty).Name}'.", nameof(propertyName));
        
        // validate the property type
        if (propertyInfo.PropertyType != typeof(TNextProperty))
            throw new InvalidPropertyTypeException(
                $"Property '{propertyName}' on type '{typeof(TTargetProperty).Name}' " +
                $"is not of type '{typeof(TNextProperty).Name}', " +
                $"but of type '{propertyInfo.PropertyType.Name}'.",
                nameof(propertyName));
        
        var thenToOptions = options.ThenTo(propertyInfo);
        
        return new AdapterPropertyThenOptionsBuilder<TProperty, TTargetProperty, TNextProperty>(thenToOptions);
    }

    /// <inheritdoc />
    public IAdapterPropertyToMethodOptionsBuilder<TTargetProperty, TProperty> ToMethod()
    {
        var toMethodOptions = options.ThenToMethod();
        return new AdapterPropertyToMethodOptionsBuilder<TTargetProperty, TProperty>(toMethodOptions);
    }

    /// <inheritdoc />
    public IAdapterPropertyToMethodOptionsBuilder<TTargetProperty, TProperty> ToMethod(
        Expression<Func<TTargetProperty, Delegate>> methodSelect)
    {
        if (!methodSelect.TryGetMethod(out var method) || !method.IsATargetMethod(typeof(TTargetProperty)))
            throw new InvalidMethodDelegateException(nameof(methodSelect));
        
        var toMethodOptions = options.ThenToMethod(method);
        return new AdapterPropertyToMethodOptionsBuilder<TTargetProperty, TProperty>(toMethodOptions);
    }
}