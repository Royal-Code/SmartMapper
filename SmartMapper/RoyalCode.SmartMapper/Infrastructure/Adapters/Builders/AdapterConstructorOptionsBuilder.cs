using RoyalCode.SmartMapper.Configurations.Adapters;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

public class AdapterConstructorOptionsBuilder<TSource> : IAdapterConstructorOptionsBuilder<TSource>
{
    private readonly AdapterOptions options;
    private readonly ConstructorOptions constructorOptions;

    public AdapterConstructorOptionsBuilder(AdapterOptions options, ConstructorOptions constructorOptions)
    {
        this.options = options;
        this.constructorOptions = constructorOptions;
    }

    public void Parameters(Action<IAdapterConstructorParametersOptionsBuilder<TSource>> configurePrameters)
    {
        var builder = new AdapterConstructorParametersOptionsBuilder<TSource>(options, constructorOptions);
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