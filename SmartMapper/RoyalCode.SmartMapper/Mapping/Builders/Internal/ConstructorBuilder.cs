using System.Linq.Expressions;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;
using RoyalCode.SmartMapper.Mapping.Options;

namespace RoyalCode.SmartMapper.Mapping.Builders.Internal;

/// <inheritdoc />
internal sealed class ConstructorBuilder<TSource> : IConstructorBuilder<TSource>
{
    private readonly SourceOptions sourceOptions;
    private readonly ConstructorOptions constructorOptions;

    /// <summary>
    /// Creates a new instance of <see cref="ConstructorBuilder{TSource}"/>.
    /// </summary>
    /// <param name="options">The adapter options.</param>
    public ConstructorBuilder(MappingOptions options)
    {
        if (options.Category is not MappingCategory.Adapter)
            throw new InvalidOperationException("To configure a constructor, the mapping must be of the Adapter category.");

        sourceOptions = options.SourceOptions;
        constructorOptions = options.TargetOptions.GetConstructorOptions();
    }

    /// <inheritdoc />
    public void Parameters(Action<IConstructorParametersBuilder<TSource>> configureParameters)
    {
        var builder = new ConstructorParametersBuilder<TSource>(sourceOptions, constructorOptions);
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
