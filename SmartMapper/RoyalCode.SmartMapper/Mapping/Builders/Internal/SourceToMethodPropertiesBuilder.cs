using RoyalCode.SmartMapper.Mapping.Options;
using RoyalCode.SmartMapper.Mapping.Options.Resolutions;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Mapping.Builders.Internal;

/// <inheritdoc />
internal sealed class SourceToMethodPropertiesBuilder<TSource> : ISourceToMethodPropertiesBuilder<TSource>
{
    private readonly SourceOptions sourceOptions;
    private readonly ToMethodResolutionOptions? parentResolutionOptions;

    private readonly SourceToMethodPropertiesOptions propertiesOptions;

    public SourceToMethodPropertiesBuilder(
        SourceOptions sourceOptions,
        SourceToMethodPropertiesOptions propertiesOptions)
    {
        this.sourceOptions = sourceOptions;
        this.propertiesOptions = propertiesOptions;
    }

    public SourceToMethodPropertiesBuilder(
        SourceOptions sourceOptions,
        SourceToMethodPropertiesOptions propertiesOptions,
        ToMethodResolutionOptions parentResolutionOptions) : this(sourceOptions, propertiesOptions)
    {
        this.parentResolutionOptions = parentResolutionOptions;
    }

    /// <inheritdoc />
    public void Ignore<TProperty>(Expression<Func<TSource, TProperty>> propertySelector)
    {
        var propertyOptions = sourceOptions.GetPropertyOptions(propertySelector);
        propertyOptions.IgnoreMapping();
    }

    /// <inheritdoc />
    public IParameterBuilder<TProperty> Parameter<TProperty>(
        Expression<Func<TSource, TProperty>> propertySelector, string? parameterName = null)
    {
        var propertyOptions = sourceOptions.GetPropertyOptions(propertySelector);
        var parameterOptions = propertiesOptions.MethodOptions.GetParameterOptions(propertyOptions.Property);

        if (parameterName is not null)
            parameterOptions.UseParameterName(parameterName);

        var resolutionOptions = ToMethodParameterResolutionOptions.Resolves(propertyOptions, parameterOptions);
        parentResolutionOptions?.AddInnerPropertyResolution(resolutionOptions);

        return new ParameterBuilder<TProperty>(resolutionOptions);
    }

    /// <inheritdoc />
    public ISourceToMethodPropertiesBuilder<TInnerProperty> InnerProperties<TInnerProperty>(
        Expression<Func<TSource, TInnerProperty>> propertySelector)
    {
        var propertyOptions = sourceOptions.GetPropertyOptions(propertySelector);
        var resolutionOptions = propertyOptions.ResolutionOptions as ToMethodResolutionOptions
            ?? ToMethodResolutionOptions.Resolvers(propertiesOptions.MethodOptions, propertyOptions);

        parentResolutionOptions?.AddInnerPropertyResolution(resolutionOptions);

        return new SourceToMethodPropertiesBuilder<TInnerProperty>(
            resolutionOptions.InnerSourceOptions,
            propertiesOptions,
            resolutionOptions);
    }

    /// <inheritdoc />
    public void InnerProperties<TInnerProperty>(
        Expression<Func<TSource, TInnerProperty>> propertySelector,
        Action<ISourceToMethodPropertiesBuilder<TInnerProperty>> configureInnerProperties)
    {
        var innerProperties = InnerProperties(propertySelector);
        configureInnerProperties(innerProperties);
    }
}