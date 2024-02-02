using RoyalCode.SmartMapper.Adapters.Options;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

internal sealed class ConstructorOptionsBuilder<TSource> : IConstructorOptionsBuilder<TSource>
{
    private readonly AdapterOptions options;
    private readonly ConstructorOptions constructorOptions;

    public ConstructorOptionsBuilder(AdapterOptions options)
    {
        this.options = options;
        constructorOptions = options.TargetOptions.GetConstructorOptions();
    }

    public void Parameters(Action<IConstructorParametersOptionsBuilder<TSource>> configurePrameters)
    {
        var builder = new ConstructorParametersOptionsBuilder<TSource>(options, constructorOptions);
        configurePrameters(builder);
    }

    public void WithParameters(int numberOfParameters)
    {
        constructorOptions.NumberOfParameters = numberOfParameters;
    }

    public void WithParameters(params Type[] parameterTypes)
    {
        constructorOptions.ParameterTypes = parameterTypes;
    }
}
