using RoyalCode.SmartMapper.Mapping.Options;
using RoyalCode.SmartMapper.Mapping.Options.Resolutions;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Mapping.Builders.Internal;

/// <inheritdoc />
internal sealed class ConstructorParametersBuilder<TSource> : IConstructorParametersBuilder<TSource>
{
    private readonly SourceOptions sourceOptions;
    private readonly ConstructorOptions constructorOptions;
    private readonly ToConstructorResolutionOptions? parentResolutionOptions;

    public ConstructorParametersBuilder(SourceOptions sourceOptions, ConstructorOptions constructorOptions)
    {
        this.sourceOptions = sourceOptions;
        this.constructorOptions = constructorOptions;
    }

    public ConstructorParametersBuilder(
        SourceOptions sourceOptions,
        ConstructorOptions constructorOptions,
        ToConstructorResolutionOptions parentResolutionOptions)
    {
        this.sourceOptions = sourceOptions;
        this.constructorOptions = constructorOptions;
        this.parentResolutionOptions = parentResolutionOptions;
    }

    public IParameterBuilder<TProperty> Parameter<TProperty>(
        Expression<Func<TSource, TProperty>> propertySelector,
        string? parameterName = null)
    {
        var propertyOptions = sourceOptions.GetPropertyOptions(propertySelector);
        var constructorParameterOptions = constructorOptions.GetParameterOptions(propertyOptions.Property);

        if (parameterName is not null)
            constructorParameterOptions.UseParameterName(parameterName);

        var resolutionOptions = ToConstructorParameterResolutionOptions.Resolves(propertyOptions, constructorParameterOptions);
        parentResolutionOptions?.AddInnerPropertyResolution(resolutionOptions);

        return new ParameterBuilder<TProperty>(resolutionOptions);
    }

    public IConstructorParametersBuilder<TInnerProperty> InnerProperties<TInnerProperty>(
        Expression<Func<TSource, TInnerProperty>> propertySelector)
    {
        var propertyOptions = sourceOptions.GetPropertyOptions(propertySelector);
        var resolutionOptions = propertyOptions.ResolutionOptions as ToConstructorResolutionOptions
            ?? ToConstructorResolutionOptions.Resolves(propertyOptions);

        parentResolutionOptions?.AddInnerPropertyResolution(resolutionOptions);

        return new ConstructorParametersBuilder<TInnerProperty>(
            resolutionOptions.InnerSourceOptions,
            constructorOptions,
            resolutionOptions);
    }

    public void InnerProperties<TInnerProperty>(
        Expression<Func<TSource, TInnerProperty>> propertySelector,
        Action<IConstructorParametersBuilder<TInnerProperty>> configureInnerProperties)
    {
        var innerBuilder = InnerProperties(propertySelector);
        configureInnerProperties(innerBuilder);
    }

    public void Ignore<TProperty>(Expression<Func<TSource, TProperty>> propertySelector)
    {
        var propertyOptions = sourceOptions.GetPropertyOptions(propertySelector);
        propertyOptions.IgnoreMapping();
    }
}
