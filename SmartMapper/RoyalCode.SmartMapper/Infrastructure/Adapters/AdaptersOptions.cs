using RoyalCode.SmartMapper.Configurations;
using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

public class AdaptersOptions : IAdaptersOptions
{
    private readonly Dictionary<MapKey, object> optionsBuilders = new ();

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
        
        builder = new AdapterOptionsBuilder<TSource, TTarget>();
        optionsBuilders.Add(key, builder);

        return (IAdapterOptionsBuilder<TSource, TTarget>)builder;
    }
}