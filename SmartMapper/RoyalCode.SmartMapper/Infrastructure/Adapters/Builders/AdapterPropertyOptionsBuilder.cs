using System.Linq.Expressions;
using System.Reflection;
using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Extensions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

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

        var toPropertyOptions = new ToPropertyOptions(typeof(TTarget), propertyInfo);
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
        
        var toPropertyOptions = new ToPropertyOptions(typeof(TTarget), propertyInfo);
        propertyOptions.MappedToProperty(toPropertyOptions);
        
        return new AdapterPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty>(
            propertyOptions, toPropertyOptions);
    }

    /// <inheritdoc />
    public IAdapterPropertyToConstructorOptionsBuilder<TProperty> ToConstructor()
    {
        var constructorOptions = adapterOptions.TargetOptions.GetConstructorOptions();
        var options = new ToConstructorOptions(propertyOptions, constructorOptions);
        propertyOptions.MappedToConstructor(options);
        
        return new AdapterPropertyToConstructorOptionsBuilder<TProperty>(options);
    }

    /// <inheritdoc />
    public IAdapterPropertyToMethodOptionsBuilder<TTarget, TProperty> ToMethod()
    {
        var methodOptions = new MethodOptions(typeof(TTarget));
        adapterOptions.TargetOptions.AddToMethod(methodOptions);
        var toMethodOptions = new ToMethodOptions(propertyOptions, methodOptions);
        propertyOptions.MappedToMethod(toMethodOptions);

        return new AdapterPropertyToMethodOptionsBuilder<TTarget, TProperty>(toMethodOptions);
    }

    /// <inheritdoc />
    public IAdapterPropertyToMethodOptionsBuilder<TTarget, TProperty> ToMethod(
        Expression<Func<TTarget, Delegate>> methodSelect)
    {
        if (!methodSelect.TryGetMethod(out var method) || !method.IsATargetMethod(typeof(TTarget)))
            throw new InvalidMethodDelegateException(nameof(methodSelect));

        var methodOptions = new MethodOptions(typeof(TTarget))
        {
            Method = method,
            MethodName = method.Name
        };
        adapterOptions.TargetOptions.AddToMethod(methodOptions);
        var toMethodOptions = new ToMethodOptions(propertyOptions, methodOptions);
        propertyOptions.MappedToMethod(toMethodOptions);
        
        return new AdapterPropertyToMethodOptionsBuilder<TTarget, TProperty>(toMethodOptions);
    }
}