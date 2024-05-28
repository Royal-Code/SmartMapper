using RoyalCode.SmartMapper.Adapters.Options;

namespace RoyalCode.SmartMapper.Core.Options;

/// <summary>
/// <para>
///     Contains all the options for a single mapping between a source and target type.
/// </para>
/// <para>
///     Holds options for adapters, selectors, and mappers.
/// </para>
/// </summary>
public sealed class MapperOptions
{
    private readonly Dictionary<(Type, Type), AdapterOptions> adapterOptions = [];

    /// <summary>
    /// Get the configured options for the adapter between TFrom and TTarget.
    /// </summary>
    /// <typeparam name="TSource">The source type</typeparam>
    /// <typeparam name="TTarget">The target type</typeparam>
    /// <returns>The adapter options</returns>
    public AdapterOptions GetAdapterOptions<TSource, TTarget>()
    {
        if (adapterOptions.TryGetValue((typeof(TSource), typeof(TTarget)), out var options))
        {
            return options;
        }

        options = new AdapterOptions(typeof(TSource), typeof(TTarget));
        adapterOptions.Add((typeof(TSource), typeof(TTarget)), options);
        return options;
    }
}
