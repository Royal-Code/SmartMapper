using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

//TODO: Ver se nome continuará ser este. Testes. Documentação.
public class AdaptersOptions : IAdaptersOptions
{
    private readonly Dictionary<MapKey, AdapterOptions> options = new();
    private readonly Dictionary<MapKey, object> optionsBuilders = new();

    /// <summary>
    /// Try get an options builder for the given key
    /// </summary>
    /// <typeparam name="TSource">
    ///     The source type.
    /// </typeparam>
    /// <typeparam name="TTarget">
    ///     The target type.
    /// </typeparam>
    /// <returns>
    ///     The options builder.
    /// </returns>
    public IAdapterOptionsBuilder<TSource, TTarget> GetOptionsBuilder<TSource, TTarget>()
    {
        var key = new MapKey(typeof(TSource), typeof(TTarget));

        if (optionsBuilders.TryGetValue(key, out var builder))
            return (IAdapterOptionsBuilder<TSource, TTarget>) builder;

        builder = new AdapterOptionsBuilder<TSource, TTarget>(GetOptions(key));
        optionsBuilders.Add(key, builder);

        return (IAdapterOptionsBuilder<TSource, TTarget>) builder;
    }

    /// <summary>
    /// Get or create an options for the given key
    /// </summary>
    /// <param name="key">The key with the source and target types</param>
    /// <returns>The options</returns>
    public AdapterOptions GetOptions(MapKey key)
    {
        if (options.TryGetValue(key, out var adapterOptions))
            return adapterOptions;

        adapterOptions = new AdapterOptions(key.SourceType, key.TargetType);
        options.Add(key, adapterOptions);
        return adapterOptions;
    }
}