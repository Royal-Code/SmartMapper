using System.Linq.Expressions;
using System.Reflection;
using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Extensions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

/// <inheritdoc />
public class AdapterPropertyOptionsBuilder<TSource, TTarget, TProperty>
    : IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty>
{
    private readonly AdapterOptions adapterOptions;
    private readonly PropertyOptions propertyOptions;

    public AdapterPropertyOptionsBuilder(AdapterOptions adapterOptions, PropertyOptions propertyOptions)
    {
        this.adapterOptions = adapterOptions;
        this.propertyOptions = propertyOptions;
    }

    /// <inheritdoc />
    public IAdapterPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> To<TTargetProperty>(
        Expression<Func<TTarget, TTargetProperty>> propertySelector)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        var toPropertyOptions = new PropertyToPropertyOptions(typeof(TTarget), propertyInfo);
        propertyOptions.MappedToProperty(toPropertyOptions);
        
        return new AdapterPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty>(
            propertyOptions, toPropertyOptions);
    }

    /// <inheritdoc />
    public IAdapterPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> To<TTargetProperty>(
        string propertyName)
    {
        // get target property by name, including inherited type properties
        var propertyInfo = typeof(TTarget).GetRuntimeProperty(propertyName);
        
        if (propertyInfo is null)
            throw new InvalidPropertyNameException(
                $"Property '{propertyName}' not found on type '{typeof(TTarget).Name}'.", nameof(propertyName));
        
        // validate the property type
        if (propertyInfo.PropertyType != typeof(TTargetProperty))
            throw new InvalidPropertyTypeException(
                $"Property '{propertyName}' on type '{typeof(TTarget).Name}' " +
                $"is not of type '{typeof(TTargetProperty).Name}', " +
                $"but of type '{propertyInfo.PropertyType.Name}'.",
                nameof(propertyName));
        
        var toPropertyOptions = new PropertyToPropertyOptions(typeof(TTarget), propertyInfo);
        propertyOptions.MappedToProperty(toPropertyOptions);
        
        return new AdapterPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty>(
            propertyOptions, toPropertyOptions);
    }

    /// <inheritdoc />
    public IAdapterPropertyToConstructorOptionsBuilder<TSource, TProperty> ToConstructor()
    {
        var constructorOptions = adapterOptions.GetConstructorOptions();
        var options = new PropertyToConstructorOptions(typeof(TProperty), constructorOptions);
        propertyOptions.MapInnerProperties(options);
        
        return new AdapterPropertyToConstructorOptionsBuilder<TSource, TProperty>(options);
    }

    /// <inheritdoc />
    public IAdapterPropertyToMethodOptionsBuilder<TSource, TTarget, TProperty> ToMethod()
    {
        var options = new PropertyToMethodOptions();
        propertyOptions.MapInnerProperties(options);

        return new AdapterPropertyToMethodOptionsBuilder<TSource, TTarget, TProperty>(options);
    }

    /// <inheritdoc />
    public IAdapterPropertyToMethodOptionsBuilder<TSource, TTarget, TProperty> ToMethod(
        Expression<Func<TTarget, Delegate>> methodSelect)
    {
        if (!methodSelect.TryGetMethod(out var method) || !method.IsATargetMethod(typeof(TTarget)))
            throw new InvalidMethodDelegateException(nameof(methodSelect));
        
        var options = new PropertyToMethodOptions()
        {
            Method = method,
            MethodName = method.Name
        };
            
        propertyOptions.MapInnerProperties(options);
        
        return new AdapterPropertyToMethodOptionsBuilder<TSource, TTarget, TProperty>(options);
    }
}