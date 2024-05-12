using RoyalCode.SmartMapper.Adapters.Options;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

internal sealed class ConstructorOptionsBuilder<TSource> : IConstructorOptionsBuilder<TSource>
{
    private readonly SourceOptions sourceOptions;
    private readonly ConstructorOptions constructorOptions;

    public ConstructorOptionsBuilder(AdapterOptions options)
    {
        sourceOptions = options.SourceOptions;
        constructorOptions = options.TargetOptions.GetConstructorOptions();
    }

    public void Parameters(Action<IConstructorParametersOptionsBuilder<TSource>> configureParameters)
    {
        var builder = new ConstructorParametersOptionsBuilder<TSource>(sourceOptions, constructorOptions);
        configureParameters(builder);
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
