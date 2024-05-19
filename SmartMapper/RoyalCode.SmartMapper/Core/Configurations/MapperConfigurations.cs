
using FastExpressionCompiler;
using RoyalCode.SmartMapper.Adapters.Configurations;
using RoyalCode.SmartMapper.Adapters.Configurations.Internal;
using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Resolutions;
using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Core.Options;
using RoyalCode.SmartMapper.Core.Gererators;
using RoyalCode.SmartMapper.Core.Discovery;

namespace RoyalCode.SmartMapper.Core.Configurations;

/// <summary>
/// <para>
///     All the configurations for the mapper, including the adapters, selectors, and other components for
///     creating the mappings.
/// </para>
/// </summary>
public class MapperConfigurations
{
    private readonly ResolutionsMap resolutionsMap = new();
    private readonly ResolutionFactory resolutionFactory;
    private readonly MapperOptions options;
    private readonly ExpressionGenerator expressionGenerator;

    /// <summary>
    /// Create a new instance of <see cref="MapperConfigurations"/>
    /// with a default <see cref="MapperOptions"/> and <see cref="ExpressionGenerator"/>.
    /// </summary>
    public MapperConfigurations() : this(new(), new()) { }
    
    /// <summary>
    /// Create a new instance of <see cref="MapperConfigurations"/>.
    /// </summary>
    /// <param name="options">The options for the mappers, adapters, and selectors.</param>
    /// <param name="expressionGenerator">Generator for creating expression trees.</param>
    /// <exception cref="ArgumentNullException">
    ///     Case <paramref name="options"/> or <paramref name="expressionGenerator"/> was null.
    /// </exception>
    public MapperConfigurations(
        MapperOptions options,
        ExpressionGenerator expressionGenerator)
    {
        this.options = options ?? throw new ArgumentNullException(nameof(options));
        this.expressionGenerator = expressionGenerator ?? throw new ArgumentNullException(nameof(expressionGenerator));

        resolutionFactory = new(this);
        Discovery = new();
    }

    /// <summary>
    /// The discovery for the mapper.
    /// </summary>
    public MapperDiscovery Discovery { get; }

    /// <summary>
    /// Component to handle the matching of member names.
    /// </summary>
    /// <exception cref="NotImplementedException">
    ///     TODO: Implement the matching of member names.
    /// </exception>
    public object MatchingNames => throw new NotImplementedException();

    /// <summary>
    /// <para>
    ///     Configure the adapter for adapting from <typeparamref name="TSource"/> to <typeparamref name="TTarget"/>.
    /// </para>
    /// </summary>
    /// <typeparam name="TSource">The source type</typeparam>
    /// <typeparam name="TTarget">The target type</typeparam>
    /// <returns>
    ///     A builder to configure the adapter options.
    /// </returns>
    public IAdapterOptionsBuilder<TSource, TTarget> ConfigureAdapter<TSource, TTarget>()
    {
        AdapterOptions adapterOptions = options.GetAdapterOptions<TSource, TTarget>();
        return new AdapterOptionsBuilder<TSource, TTarget>(adapterOptions);
    }
    
    /// <summary>
    /// Get the function that adapts from <typeparamref name="TSource"/> to <typeparamref name="TTarget"/>.
    /// </summary>
    /// <typeparam name="TSource">The source type</typeparam>
    /// <typeparam name="TTarget">The target type</typeparam>
    /// <returns>The adapter function</returns>
    public Func<TSource, TTarget> GetAdapter<TSource, TTarget>()
    {
        return resolutionsMap.GetAdapter<TSource, TTarget>()
            ?? CreateAdapter<TSource, TTarget>();
    }

    private Func<TSource, TTarget> CreateAdapter<TSource, TTarget>()
    {
        var expression = resolutionsMap.GetAdapterExpression<TSource, TTarget>();
        if (expression == null)
        {
            // Create the adapter resolution
            AdapterOptions adapterOptions = options.GetAdapterOptions<TSource, TTarget>();
            AdapterResolution resolution = resolutionFactory.CreateAdapterResolution(adapterOptions);
            resolutionsMap.AddAdapterResolution<TSource, TTarget>(resolution);

            // Create the adapter expression
            expression = expressionGenerator.CreateAdapterExpression<TSource, TTarget>(resolution);
            resolutionsMap.AddAdapterExpression(expression);
        }

        // Compile the expression
        var function = expression.CompileFast();
        resolutionsMap.AddAdapter(function);
        return function;
    }
}
