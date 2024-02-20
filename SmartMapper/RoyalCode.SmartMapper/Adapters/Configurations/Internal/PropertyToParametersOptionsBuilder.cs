using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

internal abstract class PropertyToParametersOptionsBuilderBase<TSourceProperty> 
    : IPropertyToParametersOptionsBuilder<TSourceProperty>
{
    protected readonly SourceOptions sourceOptions;

    /// <summary>
    /// Creates a new base builder to map properties to parameters.
    /// </summary>
    /// <param name="sourceOptions">The source options</param>
    protected PropertyToParametersOptionsBuilderBase(SourceOptions sourceOptions)
    {
        this.sourceOptions = sourceOptions;
    }

    public void Ignore<TProperty>(Expression<Func<TSourceProperty, TProperty>> propertySelector)
    {
        var propertyOptions = sourceOptions.GetPropertyOptions(propertySelector);
        propertyOptions.IgnoreMapping();
    }

    public abstract IToParameterOptionsBuilder<TProperty> Parameter<TProperty>(
        Expression<Func<TSourceProperty, TProperty>> propertySelector, 
        string? parameterName = null);
}

internal sealed class PropertyToParametersOptionsBuilder<TSourceProperty>
    : PropertyToParametersOptionsBuilderBase<TSourceProperty>
{
    private readonly InnerPropertiesOptions innerPropertiesOptions;
    private readonly MethodOptions methodOptions;

    public PropertyToParametersOptionsBuilder(InnerPropertiesOptions innerPropertiesOptions, MethodOptions methodOptions) 
        : base(innerPropertiesOptions.InnerSourceOptions)
    {
        this.innerPropertiesOptions = innerPropertiesOptions;
        this.methodOptions = methodOptions;
    }

    public override IToParameterOptionsBuilder<TProperty> Parameter<TProperty>(
        Expression<Func<TSourceProperty, TProperty>> propertySelector,
        string? parameterName = null)
    {
        var propertyOptions = innerPropertiesOptions.InnerSourceOptions.GetPropertyOptions(propertySelector);
        var parameterOptions = methodOptions.GetParameterOptions(propertyOptions.Property);
        
        if (parameterName is not null)
            parameterOptions.UseParameterName(parameterName);

        // when created the resolution options, the options are resolved by the resolution.
        var resolution = new ToMethodParameterResolutionOptions(propertyOptions, parameterOptions);

        var builder = new ToParameterOptionsBuilder<TProperty>(resolution);
        return builder;
    }
}