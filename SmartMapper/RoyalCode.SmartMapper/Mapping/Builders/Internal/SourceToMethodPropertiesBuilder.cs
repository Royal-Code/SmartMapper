using RoyalCode.SmartMapper.Adapters.Options.Resolutions;
using RoyalCode.SmartMapper.Mapping.Options;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Mapping.Builders.Internal;

/// <inheritdoc />
internal sealed class SourceToMethodPropertiesBuilder<TSource> : ISourceToMethodPropertiesBuilder<TSource>
{
    private readonly SourceOptions sourceOptions;
    private readonly SourceToMethodOptions sourceToMethodOptions;
    private readonly ToMethodResolutionOptions? parentResolutionOptions;

    public SourceToMethodPropertiesBuilder(
        SourceOptions sourceOptions,
        SourceToMethodOptions sourceToMethodOptions)
    {
        this.sourceOptions = sourceOptions;
        this.sourceToMethodOptions = sourceToMethodOptions;
    }

    public SourceToMethodPropertiesBuilder(
        SourceOptions sourceOptions,
        SourceToMethodOptions sourceToMethodOptions,
        ToMethodResolutionOptions parentResolutionOptions) : this(sourceOptions, sourceToMethodOptions)
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
        var parameterOptions = sourceToMethodOptions.MethodOptions.GetParameterOptions(propertyOptions.Property);

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
            ?? ToMethodResolutionOptions.Resolvers(sourceToMethodOptions.MethodOptions, propertyOptions);

        parentResolutionOptions?.AddInnerPropertyResolution(resolutionOptions);

        return new SourceToMethodPropertiesBuilder<TInnerProperty>(
            resolutionOptions.InnerSourceOptions,
            sourceToMethodOptions,
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