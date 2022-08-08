using RoyalCode.SmartMapper.Configurations.Adapters;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

/// <inheritdoc />
public class AdapterConstructorOptionsBuilder<TSource> : IAdapterConstructorOptionsBuilder<TSource>
{
    private readonly AdapterOptions options;
    private readonly ConstructorOptions constructorOptions;

    public AdapterConstructorOptionsBuilder(AdapterOptions options, ConstructorOptions constructorOptions)
    {
        this.options = options;
        this.constructorOptions = constructorOptions;
    }

    /// <inheritdoc />
    public void Parameters(Action<IAdapterConstructorParametersOptionsBuilder<TSource>> configurePrameters)
    {
        var builder = new AdapterConstructorParametersOptionsBuilder<TSource>(options, constructorOptions);
        configurePrameters(builder);
    }

    /// <inheritdoc />
    public void WithParameters(int numberOfParameters)
    {
        constructorOptions.NumberOfParameters = numberOfParameters;
    }

    /// <inheritdoc />
    public void WithParameters(params Type[] parameterTypes)
    {
        constructorOptions.ParameterTypes = parameterTypes;
    }
}