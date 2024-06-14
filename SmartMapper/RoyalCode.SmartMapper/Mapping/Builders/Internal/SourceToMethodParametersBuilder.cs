using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;
using RoyalCode.SmartMapper.Mapping.Options;
using System.Linq.Expressions;
using System.Reflection;

namespace RoyalCode.SmartMapper.Mapping.Builders.Internal;

/// <inheritdoc />
internal sealed class SourceToMethodParametersBuilder<TSource> : ISourceToMethodParametersBuilder<TSource>
{
    private readonly SourceOptions sourceOptions;
    private readonly SourceToMethodParametersOptions parametersOptions;

    public SourceToMethodParametersBuilder(
        SourceOptions sourceOptions,
        SourceToMethodParametersOptions parametersOptions)
    {
        this.sourceOptions = sourceOptions;
        this.parametersOptions = parametersOptions;
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

        var resolution = parametersOptions.AddPropertyToParameterSequence(propertyOptions);

        return new ParameterBuilder<TProperty>(resolution);
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
