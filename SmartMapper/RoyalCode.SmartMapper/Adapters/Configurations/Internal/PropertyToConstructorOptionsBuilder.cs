using RoyalCode.SmartMapper.Adapters.Options.Resolutions;
using RoyalCode.SmartMapper.Mapping.Builders;
using RoyalCode.SmartMapper.Mapping.Options;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

internal sealed class PropertyToConstructorOptionsBuilder<TProperty> : IPropertyToConstructorOptionsBuilder<TProperty>
{
    private readonly ToConstructorResolutionOptions toConstructorResolutionOptions;
    private readonly SourceOptions sourceOptions;
    private readonly ConstructorOptions constructorOptions;

    public PropertyToConstructorOptionsBuilder(
        ToConstructorResolutionOptions toConstructorResolutionOptions,
        AdapterOptions adapterOptions)
    {
        this.toConstructorResolutionOptions = toConstructorResolutionOptions;
        sourceOptions = adapterOptions.SourceOptions;
        constructorOptions = adapterOptions.TargetOptions.GetConstructorOptions();
    }

    public void Parameters(Action<IConstructorParametersBuilder<TProperty>> configurePrameters)
    {
        var builder = new ConstructorParametersOptionsBuilder<TProperty>(
            sourceOptions, 
            constructorOptions, 
            toConstructorResolutionOptions);

        configurePrameters(builder);
    }
}
