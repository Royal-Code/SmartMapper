using System.Linq.Expressions;
using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

/// <inheritdoc />
public class InnerPropertiesOptionsBuilder<TSource, TTarget, TProperty> : IInnerPropertiesOptionsBuilder<TSource, TTarget, TProperty>
{
    private readonly AdapterOptions adapterOptions;
    private readonly InnerPropertiesResolutionOptions propertiesResolutionOptions;

    /// <summary>
    /// Creates a new instance of <see cref="InnerPropertiesOptionsBuilder{TSource, TTarget, TProperty}"/>.
    /// </summary>
    /// <param name="adapterOptions"></param>
    /// <param name="propertiesResolutionOptions"></param>
    public InnerPropertiesOptionsBuilder(
        AdapterOptions adapterOptions,
        InnerPropertiesResolutionOptions propertiesResolutionOptions)
    {
        this.adapterOptions = adapterOptions;
        this.propertiesResolutionOptions = propertiesResolutionOptions;
    }
    
    /// <inheritdoc />
    public IPropertyOptionsBuilder<TProperty, TTarget, TInnerProperty> Map<TInnerProperty>(
        Expression<Func<TProperty, TInnerProperty>> propertySelector)
    {
        var innerAdapterOptions = new AdapterOptions(
            propertiesResolutionOptions.InnerSourceOptions,
            adapterOptions.TargetOptions);
        
        var innerPropertyOptions = innerAdapterOptions.SourceOptions.GetPropertyOptions(propertySelector);

        return new PropertyOptionsBuilder<TProperty, TTarget, TInnerProperty>(innerAdapterOptions, innerPropertyOptions);
    }

    /// <inheritdoc />
    public IPropertyOptionsBuilder<TProperty, TTarget, TInnerProperty> Map<TInnerProperty>(string propertyName)
    {
        var innerAdapterOptions = new AdapterOptions(
            propertiesResolutionOptions.InnerSourceOptions,
            adapterOptions.TargetOptions);
        
        var innerPropertyOptions = innerAdapterOptions.SourceOptions.GetPropertyOptions(propertyName, typeof(TProperty));
       
        return new PropertyOptionsBuilder<TProperty, TTarget, TInnerProperty>(innerAdapterOptions, innerPropertyOptions);
    }

    /// <inheritdoc />
    public void Ignore<TInnerProperty>(Expression<Func<TSource, TInnerProperty>> propertySelector)
    {
        var innerPropertyOptions = propertiesResolutionOptions.InnerSourceOptions.GetPropertyOptions(propertySelector);
        IgnoreResolutionOptions.Resolves(innerPropertyOptions);
    }

    /// <inheritdoc />
    public void Ignore(string propertyName)
    {
        var innerPropertyOptions = propertiesResolutionOptions.InnerSourceOptions.GetPropertyOptions(propertyName);
        IgnoreResolutionOptions.Resolves(innerPropertyOptions);
    }
}