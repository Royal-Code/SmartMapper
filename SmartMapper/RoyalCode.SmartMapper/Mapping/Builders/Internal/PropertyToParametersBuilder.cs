using RoyalCode.SmartMapper.Mapping.Options;
using RoyalCode.SmartMapper.Mapping.Options.Resolutions;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Mapping.Builders.Internal;

internal sealed class PropertyToParametersBuilder<TSourceProperty>
    : IPropertyToParametersBuilder<TSourceProperty>
{
    private readonly SourceOptions propertySourceOptions;
    private readonly MethodOptions methodOptions;

    public PropertyToParametersBuilder(SourceOptions propertySourceOptions, MethodOptions methodOptions)
    {
        this.propertySourceOptions = propertySourceOptions;
        this.methodOptions = methodOptions;
    }

    public IParameterBuilder<TProperty> Parameter<TProperty>(
        Expression<Func<TSourceProperty, TProperty>> propertySelector,
        string? parameterName = null)
    {
        var propertyOptions = propertySourceOptions.GetPropertyOptions(propertySelector);
        var parameterOptions = methodOptions.GetParameterOptions(propertyOptions.Property);

        if (parameterName is not null)
            parameterOptions.UseParameterName(parameterName);

        // when created the resolution options, the options are resolved by the resolution.
        var resolution = ToMethodParameterResolutionOptions.Resolves(propertyOptions, parameterOptions);

        var builder = new ParameterBuilder<TProperty>(resolution);
        return builder;
    }

    public void Ignore<TProperty>(Expression<Func<TSourceProperty, TProperty>> propertySelector)
    {
        var propertyOptions = propertySourceOptions.GetPropertyOptions(propertySelector);
        propertyOptions.IgnoreMapping();
    }
}