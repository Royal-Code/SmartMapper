using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

internal sealed class PropertyToConstructorOptionsBuilder<TProperty> : IPropertyToConstructorOptionsBuilder<TProperty>
{
    private readonly ToConstructorResolutionOptions toConstructorResolutionOptions;
    private readonly AdapterOptions adapterOptions;
    private readonly ConstructorOptions constructorOptions;

    public PropertyToConstructorOptionsBuilder(
        ToConstructorResolutionOptions toConstructorResolutionOptions,
        AdapterOptions adapterOptions)
    {
        this.toConstructorResolutionOptions = toConstructorResolutionOptions;
        this.adapterOptions = adapterOptions;
        constructorOptions = adapterOptions.TargetOptions.GetConstructorOptions();
    }

    public void Parameters(Action<IConstructorParametersOptionsBuilder<TProperty>> configurePrameters)
    {
        var builder = new ConstructorParametersOptionsBuilder<TProperty>(
            adapterOptions, 
            constructorOptions, 
            toConstructorResolutionOptions);

        configurePrameters(builder);
    }
}
