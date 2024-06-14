using System.Linq.Expressions;
using RoyalCode.SmartMapper.Mapping.Options;
using RoyalCode.SmartMapper.Mapping.Options.Resolutions;

namespace RoyalCode.SmartMapper.Mapping.Builders.Internal;

/// <inheritdoc />
public class InnerPropertiesBuilder<TSource, TTarget, TProperty> : IInnerPropertiesBuilder<TSource, TTarget, TProperty>
{
    private readonly TargetOptions targetOptions;
    private readonly InnerPropertiesResolutionOptions propertiesResolutionOptions;

    /// <summary>
    /// Creates a new instance of <see cref="InnerPropertiesBuilder{TSource, TTarget, TProperty}"/>.
    /// </summary>
    /// <param name="targetOptions"></param>
    /// <param name="propertiesResolutionOptions"></param>
    public InnerPropertiesBuilder(
        TargetOptions targetOptions,
        InnerPropertiesResolutionOptions propertiesResolutionOptions)
    {
        this.targetOptions = targetOptions;
        this.propertiesResolutionOptions = propertiesResolutionOptions;
    }

    /// <inheritdoc />
    public IPropertyBuilder<TProperty, TTarget, TInnerProperty> Map<TInnerProperty>(
        Expression<Func<TProperty, TInnerProperty>> propertySelector)
    {
        var innerAdapterOptions = new MappingOptions(
            propertiesResolutionOptions.InnerSourceOptions,
            targetOptions);

        var innerPropertyOptions = innerAdapterOptions.SourceOptions.GetPropertyOptions(propertySelector);

        return new PropertyBuilder<TProperty, TTarget, TInnerProperty>(targetOptions, innerPropertyOptions);
    }

    /// <inheritdoc />
    public IPropertyBuilder<TProperty, TTarget, TInnerProperty> Map<TInnerProperty>(string propertyName)
    {
        var innerAdapterOptions = new MappingOptions(
            propertiesResolutionOptions.InnerSourceOptions,
            targetOptions);

        var innerPropertyOptions = innerAdapterOptions.SourceOptions.GetPropertyOptions(propertyName, typeof(TProperty));

        return new PropertyBuilder<TProperty, TTarget, TInnerProperty>(targetOptions, innerPropertyOptions);
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