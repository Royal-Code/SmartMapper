using System.Linq.Expressions;
using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;
using RoyalCode.SmartMapper.Mapping.Options;
using RoyalCode.SmartMapper.Mapping.Options.Resolutions;

namespace RoyalCode.SmartMapper.Mapping.Builders.Internal;

/// <inheritdoc />
internal sealed class PropertyBuilder<TSource, TTarget, TProperty> 
    : IPropertyBuilder<TSource, TTarget, TProperty>
{
    private readonly TargetOptions tagerOptions;
    private readonly PropertyOptions propertyOptions;

    /// <summary>
    /// Create a new instance of <see cref="PropertyBuilder{TSource, TTarget, TProperty}"/>
    /// </summary>
    /// <param name="tagerOptions">The target options</param>
    /// <param name="propertyOptions">The source property options</param>
    public PropertyBuilder(TargetOptions tagerOptions, PropertyOptions propertyOptions)
    {
        this.tagerOptions = tagerOptions;
        this.propertyOptions = propertyOptions;
    }
    
    /// <inheritdoc />
    public IPropertyToPropertyBuilder<TSource, TTarget, TProperty, TTargetProperty> To<TTargetProperty>(
        Expression<Func<TTarget, TTargetProperty>> propertySelector)
    {
        var toTargetPropertyOptions = tagerOptions.GetToTargetPropertyOptions(propertySelector);
        
        var resolution = ToPropertyResolutionOptions.Resolves(propertyOptions, toTargetPropertyOptions);

        var builder = new PropertyToPropertyBuilder<TSource, TTarget, TProperty, TTargetProperty>(resolution);
        
        return builder;
    }

    /// <inheritdoc />
    public IPropertyToPropertyBuilder<TSource, TTarget, TProperty, TTargetProperty> To<TTargetProperty>(
        string propertyName)
    {
        var toTargetPropertyOptions = tagerOptions
            .GetToTargetPropertyOptions(propertyName, typeof(TTargetProperty));
        
        var resolution = ToPropertyResolutionOptions.Resolves(propertyOptions, toTargetPropertyOptions);

        var builder = new PropertyToPropertyBuilder<TSource, TTarget, TProperty, TTargetProperty>(resolution);

        return builder;
    }

    /// <inheritdoc />
    public void InnerProperties(Action<IInnerPropertiesBuilder<TSource, TTarget, TProperty>>? innerPropertiesBuilder = null)
    {
        // create a resolution options for inner properties
        var resolutionOptions = InnerPropertiesResolutionOptions.Resolves(propertyOptions);
        
        if (innerPropertiesBuilder == null)
            return;
        
        var builder = new InnerPropertiesBuilder<TSource, TTarget, TProperty>(tagerOptions, resolutionOptions);
        innerPropertiesBuilder(builder);
    }

    /// <inheritdoc />
    public IPropertyToMethodBuilder<TTarget, TProperty> ToMethod()
    {
        var methodOptions = tagerOptions.CreateMethodOptions();
        var resolution = PropertyToMethodResolutionOptions.Resolves(propertyOptions, methodOptions);
        var builder = new PropertyToMethodBuilder<TTarget, TProperty>(resolution);
        return builder;
    }

    /// <inheritdoc />
    public IPropertyToMethodBuilder<TTarget, TProperty> ToMethod(
        Expression<Func<TTarget, Delegate>> methodSelector)
    {
        if (!methodSelector.TryGetMethod(out var method))
            throw new InvalidMethodDelegateException(nameof(methodSelector));

        var methodOptions = tagerOptions.CreateMethodOptions();
        methodOptions.Method = method;
        methodOptions.MethodName = method.Name;
        var resolution = PropertyToMethodResolutionOptions.Resolves(propertyOptions, methodOptions);
        var builder = new PropertyToMethodBuilder<TTarget, TProperty>(resolution);
        return builder;
    }
}