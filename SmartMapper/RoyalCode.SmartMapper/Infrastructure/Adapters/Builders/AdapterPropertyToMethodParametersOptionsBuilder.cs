using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

/// <summary>
/// <para>
///     A builder to configure the mapping of the internal properties of a source property 
///     to parameters of a target method.
/// </para>
/// </summary>
/// <typeparam name="TSourceProperty">The type of the source property.</typeparam>
public class AdapterPropertyToMethodParametersOptionsBuilder<TSourceProperty>
    : AdapterPropertyToParametersOptionsBuilder<TSourceProperty, ToMethodParameterOptions>
{
    public AdapterPropertyToMethodParametersOptionsBuilder(SourceOptions sourceOptions, MethodOptions methodOptions)
        : base(sourceOptions, methodOptions)
    { }

    /// <inheritdoc />
    protected override void Map(PropertyOptions propertyOptions, ToMethodParameterOptions parameterOptions)
    {
        propertyOptions.MappedToMethodParameter(parameterOptions);
    }
}