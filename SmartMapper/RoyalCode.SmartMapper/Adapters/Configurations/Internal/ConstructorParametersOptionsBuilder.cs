using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;
using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;
using System.Linq.Expressions;
using System.Reflection;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

internal sealed class ConstructorParametersOptionsBuilder<TSource> : IConstructorParametersOptionsBuilder<TSource>
{
    private readonly AdapterOptions adapterOptions;
    private readonly ConstructorOptions constructorOptions;
    private readonly ToConstructorResolutionOptions? parentResolutionOptions;

    public ConstructorParametersOptionsBuilder(AdapterOptions adapterOptions, ConstructorOptions constructorOptions)
    {
        this.adapterOptions = adapterOptions;
        this.constructorOptions = constructorOptions;
    }

    public ConstructorParametersOptionsBuilder(
        AdapterOptions adapterOptions, 
        ConstructorOptions constructorOptions, 
        ToConstructorResolutionOptions parentResolutionOptions)
    {
        this.adapterOptions = adapterOptions;
        this.constructorOptions = constructorOptions;
        this.parentResolutionOptions = parentResolutionOptions;
    }

    public IToParameterOptionsBuilder<TProperty> Parameter<TProperty>(
        Expression<Func<TSource, TProperty>> propertySelector, 
        string? parameterName = null)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(propertyInfo);
        var constructorParameterOptions = constructorOptions.GetParameterOptions(propertyInfo);
        
        if (parameterName is not null)
            constructorParameterOptions.UseParameterName(parameterName);

        var resolutionOptions = new ToConstructorParameterResolutionOptions(propertyOptions, constructorParameterOptions);
        parentResolutionOptions?.AddInnerPropertyResolution(resolutionOptions);

        return new ToParameterOptionsBuilder<TProperty>(resolutionOptions);
    }

    public IConstructorParametersOptionsBuilder<TInnerProperty> InnerProperties<TInnerProperty>(
        Expression<Func<TSource, TInnerProperty>> propertySelector)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(propertyInfo);
        
        var resolutionOptions = propertyOptions.ResolutionOptions is ToConstructorResolutionOptions tcro
            ? tcro
            : new ToConstructorResolutionOptions(propertyOptions);

        parentResolutionOptions?.AddInnerPropertyResolution(resolutionOptions);

        var innerAdapterOptions = new AdapterOptions(resolutionOptions.InnerSourceOptions, adapterOptions.TargetOptions);
        return new ConstructorParametersOptionsBuilder<TInnerProperty>(innerAdapterOptions, constructorOptions, resolutionOptions);
    }

    public void InnerProperties<TInnerProperty>(
        Expression<Func<TSource, TInnerProperty>> propertySelector, 
        Action<IConstructorParametersOptionsBuilder<TInnerProperty>> configureInnerProperties)
    {
        var innerBuilder = InnerProperties(propertySelector);
        configureInnerProperties(innerBuilder);
    }
}
