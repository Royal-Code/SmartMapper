using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

/// <inheritdoc />
internal sealed class SourceToMethodPropertiesOptionsBuilder<TSource> : ISourceToMethodPropertiesOptionsBuilder<TSource>
{
    private readonly AdapterOptions adapterOptions;
    private readonly SourceToMethodOptions sourceToMethodOptions;
    private readonly ToMethodResolutionOptions? parentResolutionOptions;

    public SourceToMethodPropertiesOptionsBuilder(
        AdapterOptions adapterOptions,
        SourceToMethodOptions sourceToMethodOptions)
    {
        this.adapterOptions = adapterOptions;
        this.sourceToMethodOptions = sourceToMethodOptions;
    }

    public SourceToMethodPropertiesOptionsBuilder(
        AdapterOptions adapterOptions,
        SourceToMethodOptions sourceToMethodOptions,
        ToMethodResolutionOptions parentResolutionOptions) : this(adapterOptions, sourceToMethodOptions)
    {
        this.parentResolutionOptions = parentResolutionOptions;
    }

    /// <inheritdoc />
    public void Ignore<TProperty>(Expression<Func<TSource, TProperty>> propertySelector)
    {
        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(propertySelector);
        propertyOptions.IgnoreMapping();
    }

    /// <inheritdoc />
    public IToParameterOptionsBuilder<TProperty> Parameter<TProperty>(
        Expression<Func<TSource, TProperty>> propertySelector, string? parameterName = null)
    {
        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(propertySelector);
        var parameterOptions = sourceToMethodOptions.MethodOptions.GetParameterOptions(propertyOptions.Property);

        if (parameterName is not null)
            parameterOptions.UseParameterName(parameterName);

        var resolutionOptions = new ToMethodParameterResolutionOptions(propertyOptions, parameterOptions);
        parentResolutionOptions?.AddInnerPropertyResolution(resolutionOptions);

        return new ToParameterOptionsBuilder<TProperty>(resolutionOptions);
    }

    /// <inheritdoc />
    public ISourceToMethodPropertiesOptionsBuilder<TInnerProperty> InnerProperties<TInnerProperty>(
        Expression<Func<TSource, TInnerProperty>> propertySelector)
    {
        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(propertySelector);
        var resolutionOptions = propertyOptions.ResolutionOptions is ToMethodResolutionOptions tcro
            ? tcro
            : new ToMethodResolutionOptions(sourceToMethodOptions.MethodOptions, propertyOptions);

        parentResolutionOptions?.AddInnerPropertyResolution(resolutionOptions);

        var innerAdapterOptions = new AdapterOptions(resolutionOptions.InnerSourceOptions, adapterOptions.TargetOptions);
        return new SourceToMethodPropertiesOptionsBuilder<TInnerProperty>(innerAdapterOptions, sourceToMethodOptions, resolutionOptions);
    }

    /// <inheritdoc />
    public void InnerProperties<TInnerProperty>(
        Expression<Func<TSource, TInnerProperty>> propertySelector,
        Action<ISourceToMethodPropertiesOptionsBuilder<TInnerProperty>> configureInnerProperties)
    {
        var innerProperties = InnerProperties(propertySelector);
        configureInnerProperties(innerProperties);
    }
}