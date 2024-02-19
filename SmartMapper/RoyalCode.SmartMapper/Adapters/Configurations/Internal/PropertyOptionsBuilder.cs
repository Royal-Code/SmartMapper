using System.Linq.Expressions;
using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

/// <inheritdoc />
internal sealed class PropertyOptionsBuilder<TSource, TTarget, TProperty> 
    : IPropertyOptionsBuilder<TSource, TTarget, TProperty>
{
    private readonly AdapterOptions adapterOptions;
    private readonly PropertyOptions propertyOptions;

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
        
        var resolution = new ToPropertyResolutionOptions(propertyOptions, toTargetPropertyOptions);

        var builder = new PropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty>(
            adapterOptions, 
            resolution);
        
        return builder;
    }

    /// <inheritdoc />
    public IPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> To<TTargetProperty>(
        string propertyName)
    {
        var toTargetPropertyOptions = adapterOptions.TargetOptions
            .GetToTargetPropertyOptions(propertyName, typeof(TTargetProperty));
        
        var resolution = new ToPropertyResolutionOptions(propertyOptions, toTargetPropertyOptions);

        var builder = new PropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty>(
            adapterOptions,
            resolution);

        return builder;
    }

    /// <inheritdoc />
    public IPropertyToConstructorOptionsBuilder<TProperty> ToConstructor()
    {
        var resolutionOptions = new ToConstructorResolutionOptions(propertyOptions);
        var innerAdapterOptions = new AdapterOptions(resolutionOptions.InnerSourceOptions, adapterOptions.TargetOptions);
        return new PropertyToConstructorOptionsBuilder<TProperty>(resolutionOptions, innerAdapterOptions);
    }

    /// <inheritdoc />
    public IPropertyToMethodOptionsBuilder<TTarget, TProperty> ToMethod()
    {
        var methodOptions = adapterOptions.TargetOptions.CreateMethodOptions();
        var resolution = new ToMethodResolutionOptions(methodOptions, propertyOptions);

        var toMethodOptions = new ToMethodOptions(resolution);
        var builder = new PropertyToMethodOptionsBuilder<TTarget, TProperty>(resolution);

        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IPropertyToMethodOptionsBuilder<TTarget, TProperty> ToMethod(
        Expression<Func<TTarget, Delegate>> methodSelect)
    {
        throw new NotImplementedException();
    }
}