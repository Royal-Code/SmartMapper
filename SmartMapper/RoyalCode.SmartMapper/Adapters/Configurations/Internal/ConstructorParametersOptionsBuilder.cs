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

    public ConstructorParametersOptionsBuilder(AdapterOptions adapterOptions, ConstructorOptions constructorOptions)
    {
        this.adapterOptions = adapterOptions;
        this.constructorOptions = constructorOptions;
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

        var resolutionOptions = new ToParameterResolutionOptions(propertyOptions, constructorParameterOptions);

        return new ToParameterOptionsBuilder<TProperty>(resolutionOptions);
    }
}
