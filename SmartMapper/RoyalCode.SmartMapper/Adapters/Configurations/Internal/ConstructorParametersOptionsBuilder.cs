using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

internal sealed class ConstructorParametersOptionsBuilder<TSource> : IConstructorParametersOptionsBuilder<TSource>
{
    private readonly SourceOptions sourceOptions;
    private readonly ConstructorOptions constructorOptions;
    private readonly ToConstructorResolutionOptions? parentResolutionOptions;

    public ConstructorParametersOptionsBuilder(SourceOptions sourceOptions, ConstructorOptions constructorOptions)
    {
        this.sourceOptions = sourceOptions;
        this.constructorOptions = constructorOptions;
    }

    public ConstructorParametersOptionsBuilder(
        SourceOptions sourceOptions, 
        ConstructorOptions constructorOptions, 
        ToConstructorResolutionOptions parentResolutionOptions)
    {
        this.sourceOptions = sourceOptions;
        this.constructorOptions = constructorOptions;
        this.parentResolutionOptions = parentResolutionOptions;
    }

    public IToParameterOptionsBuilder<TProperty> Parameter<TProperty>(
        Expression<Func<TSource, TProperty>> propertySelector, 
        string? parameterName = null)
    {
        var propertyOptions = sourceOptions.GetPropertyOptions(propertySelector);
        var constructorParameterOptions = constructorOptions.GetParameterOptions(propertyOptions.Property);
        
        if (parameterName is not null)
            constructorParameterOptions.UseParameterName(parameterName);

        var resolutionOptions = new ToConstructorParameterResolutionOptions(propertyOptions, constructorParameterOptions);
        parentResolutionOptions?.AddInnerPropertyResolution(resolutionOptions);

        return new ToParameterOptionsBuilder<TProperty>(resolutionOptions);
    }

    public IConstructorParametersOptionsBuilder<TInnerProperty> InnerProperties<TInnerProperty>(
        Expression<Func<TSource, TInnerProperty>> propertySelector)
    {
        var propertyOptions = sourceOptions.GetPropertyOptions(propertySelector);
        var resolutionOptions = propertyOptions.ResolutionOptions is ToConstructorResolutionOptions tcro
            ? tcro
            : new ToConstructorResolutionOptions(propertyOptions);

        parentResolutionOptions?.AddInnerPropertyResolution(resolutionOptions);

        return new ConstructorParametersOptionsBuilder<TInnerProperty>(
            resolutionOptions.InnerSourceOptions, 
            constructorOptions,
            resolutionOptions);
    }

    public void InnerProperties<TInnerProperty>(
        Expression<Func<TSource, TInnerProperty>> propertySelector, 
        Action<IConstructorParametersOptionsBuilder<TInnerProperty>> configureInnerProperties)
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
