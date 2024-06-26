﻿using System.Linq.Expressions;
using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;
using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

/// <inheritdoc />
internal sealed class PropertyOptionsBuilder<TSource, TTarget, TProperty> 
    : IPropertyOptionsBuilder<TSource, TTarget, TProperty>
{
    private readonly AdapterOptions adapterOptions;
    private readonly PropertyOptions propertyOptions;

    /// <summary>
    /// Create a new instance of <see cref="PropertyOptionsBuilder{TSource, TTarget, TProperty}"/>
    /// </summary>
    /// <param name="adapterOptions">The adapter options</param>
    /// <param name="propertyOptions">The source property options</param>
    public PropertyOptionsBuilder(AdapterOptions adapterOptions, PropertyOptions propertyOptions)
    {
        this.adapterOptions = adapterOptions;
        this.propertyOptions = propertyOptions;
    }
    
    /// <inheritdoc />
    public IPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> To<TTargetProperty>(
        Expression<Func<TTarget, TTargetProperty>> propertySelector)
    {
        var toTargetPropertyOptions = adapterOptions.TargetOptions.GetToTargetPropertyOptions(propertySelector);
        
        var resolution = ToPropertyResolutionOptions.Resolves(propertyOptions, toTargetPropertyOptions);

        var builder = new PropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty>(resolution);
        
        return builder;
    }

    /// <inheritdoc />
    public IPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> To<TTargetProperty>(
        string propertyName)
    {
        var toTargetPropertyOptions = adapterOptions.TargetOptions
            .GetToTargetPropertyOptions(propertyName, typeof(TTargetProperty));
        
        var resolution = ToPropertyResolutionOptions.Resolves(propertyOptions, toTargetPropertyOptions);

        var builder = new PropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty>(resolution);

        return builder;
    }

    /// <inheritdoc />
    public void InnerProperties(Action<IInnerPropertiesOptionsBuilder<TSource, TTarget, TProperty>>? innerPropertiesBuilder = null)
    {
        // create a resolution options for inner properties
        var resolutionOptions = InnerPropertiesResolutionOptions.Resolves(propertyOptions);
        
        if (innerPropertiesBuilder == null)
            return;
        
        var builder = new InnerPropertiesOptionsBuilder<TSource, TTarget, TProperty>(adapterOptions, resolutionOptions);
        innerPropertiesBuilder(builder);
    }

    /// <inheritdoc />
    public IPropertyToConstructorOptionsBuilder<TProperty> ToConstructor()
    {
        var resolutionOptions = ToConstructorResolutionOptions.Resolves(propertyOptions);
        var innerAdapterOptions = new AdapterOptions(resolutionOptions.InnerSourceOptions, adapterOptions.TargetOptions);
        return new PropertyToConstructorOptionsBuilder<TProperty>(resolutionOptions, innerAdapterOptions);
    }

    /// <inheritdoc />
    public IPropertyToMethodOptionsBuilder<TTarget, TProperty> ToMethod()
    {
        var methodOptions = adapterOptions.TargetOptions.CreateMethodOptions();
        var resolution = PropertyToMethodResolutionOptions.Resolves(propertyOptions, methodOptions);
        var builder = new PropertyToMethodOptionsBuilder<TTarget, TProperty>(resolution);
        return builder;
    }

    /// <inheritdoc />
    public IPropertyToMethodOptionsBuilder<TTarget, TProperty> ToMethod(
        Expression<Func<TTarget, Delegate>> methodSelector)
    {
        if (!methodSelector.TryGetMethod(out var method))
            throw new InvalidMethodDelegateException(nameof(methodSelector));

        var methodOptions = adapterOptions.TargetOptions.CreateMethodOptions();
        methodOptions.Method = method;
        methodOptions.MethodName = method.Name;
        var resolution = PropertyToMethodResolutionOptions.Resolves(propertyOptions, methodOptions);
        var builder = new PropertyToMethodOptionsBuilder<TTarget, TProperty>(resolution);
        return builder;
    }
}