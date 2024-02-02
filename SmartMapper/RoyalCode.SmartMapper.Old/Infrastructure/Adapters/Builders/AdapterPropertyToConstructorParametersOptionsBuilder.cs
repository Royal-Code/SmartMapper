using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

/// <summary>
/// <para>
///     A builder to configure the mapping of the internal properties of a source property 
///     to parameters of a target constructor.
/// </para>
/// </summary>
/// <typeparam name="TSourceProperty">The type of the source property.</typeparam>
public class AdapterPropertyToConstructorParametersOptionsBuilder<TSourceProperty>
    : AdapterPropertyToParametersOptionsBuilder<TSourceProperty, ToConstructorParameterOptions>
{
    public AdapterPropertyToConstructorParametersOptionsBuilder(
        SourceOptions sourceOptions, ConstructorOptions constructorOptions)
        : base(sourceOptions, constructorOptions)
    { }

    /// <inheritdoc />
    protected override void Map(PropertyOptions propertyOptions, ToConstructorParameterOptions parameterOptions)
    {
        propertyOptions.MappedToConstructorParameter(parameterOptions);
    }
}