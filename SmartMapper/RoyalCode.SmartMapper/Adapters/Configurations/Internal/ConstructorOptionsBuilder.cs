using System.Linq.Expressions;
using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

/// <inheritdoc />
internal sealed class ConstructorOptionsBuilder<TSource> : IConstructorOptionsBuilder<TSource>
{
    private readonly SourceOptions sourceOptions;
    private readonly ConstructorOptions constructorOptions;

    /// <summary>
    /// Creates a new instance of <see cref="ConstructorOptionsBuilder{TSource}"/>.
    /// </summary>
    /// <param name="options">The adapter options.</param>
    public ConstructorOptionsBuilder(AdapterOptions options)
    {
        sourceOptions = options.SourceOptions;
        constructorOptions = options.TargetOptions.GetConstructorOptions();
    }

    /// <inheritdoc />
    public void Parameters(Action<IConstructorParametersOptionsBuilder<TSource>> configureParameters)
    {
        var builder = new ConstructorParametersOptionsBuilder<TSource>(sourceOptions, constructorOptions);
        configureParameters(builder);
    }

    /// <inheritdoc />
    public void Map(params Expression<Func<TSource, object>>[] propertySelectors)
    {
        var index = 0;
        foreach (var propertySelector in propertySelectors)
        {
            var propertyOptions = sourceOptions.GetPropertyOptions(propertySelector);
            var constructorParameterOptions = constructorOptions.GetParameterOptions(propertyOptions.Property);
            var resolutionOptions = ToConstructorParameterResolutionOptions
                .Resolves(propertyOptions, constructorParameterOptions);
            resolutionOptions.Sequence = index++;
        }
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
