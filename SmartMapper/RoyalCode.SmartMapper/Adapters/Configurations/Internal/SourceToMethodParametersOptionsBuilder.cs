using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;
using RoyalCode.SmartMapper.Mapping.Builders;
using RoyalCode.SmartMapper.Mapping.Options;
using System.Linq.Expressions;
using System.Reflection;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

internal sealed class SourceToMethodParametersOptionsBuilder<TSource> : ISourceToMethodParametersOptionsBuilder<TSource>
{
    private readonly SourceOptions sourceOptions;
    private readonly SourceToMethodOptions sourceToMethodOptions;

    public SourceToMethodParametersOptionsBuilder(
        SourceOptions sourceOptions,
        SourceToMethodOptions sourceToMethodOptions)
    {
        this.sourceOptions = sourceOptions;
        this.sourceToMethodOptions = sourceToMethodOptions;
    }

    public void Ignore<TProperty>(Expression<Func<TSource, TProperty>> propertySelector)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        var propertyOptions = sourceOptions.GetPropertyOptions(propertyInfo);

        propertyOptions.IgnoreMapping();
    }

    public IParameterBuilder<TProperty> Parameter<TProperty>(
        Expression<Func<TSource, TProperty>> propertySelector)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        var propertyOptions = sourceOptions.GetPropertyOptions(propertyInfo);

        var resolution = sourceToMethodOptions.AddPropertyToParameterSequence(propertyOptions);

        return new ToParameterOptionsBuilder<TProperty>(resolution);
    }

    // TODO: REQUER IMPLEMENTAÇÃO

    /////// <inheritdoc />
    ////public ISourceToMethodPropertiesOptionsBuilder<TInnerProperty> InnerProperties<TInnerProperty>(
    ////    Expression<Func<TSource, TInnerProperty>> propertySelector)
    ////{
    ////    var propertyOptions = sourceOptions.GetPropertyOptions(propertySelector);
    ////    var resolutionOptions = propertyOptions.ResolutionOptions as ToMethodResolutionOptions
    ////        ?? ToMethodResolutionOptions.Resolvers(sourceToMethodOptions.MethodOptions, propertyOptions);

    ////    parentResolutionOptions?.AddInnerPropertyResolution(resolutionOptions);

    ////    return new SourceToMethodPropertiesOptionsBuilder<TInnerProperty>(
    ////        resolutionOptions.InnerSourceOptions,
    ////        sourceToMethodOptions,
    ////        resolutionOptions);
    ////}
}
