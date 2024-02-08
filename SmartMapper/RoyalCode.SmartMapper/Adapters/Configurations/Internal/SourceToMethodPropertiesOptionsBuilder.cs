using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;
using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;
using System.Linq.Expressions;
using System.Reflection;

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
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(propertyInfo);
        propertyOptions.IgnoreMapping();
    }

    /// <inheritdoc />
    public IToParameterOptionsBuilder<TProperty> Parameter<TProperty>(
        Expression<Func<TSource, TProperty>> propertySelector, string? parameterName = null)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(propertyInfo);
        var parameterOptions = sourceToMethodOptions.MethodOptions.GetParameterOptions(propertyInfo);

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
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(propertyInfo);

        var resolutionOptions = propertyOptions.ResolutionOptions is ToMethodResolutionOptions tcro
            ? tcro
            : new ToMethodResolutionOptions(propertyOptions);

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