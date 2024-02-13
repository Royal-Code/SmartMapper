
using RoyalCode.SmartMapper.Adapters.Resolutions;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Core.Resolutions;

/// <summary>
/// <para>
///     A map of resolutions for the mappers, adapters, and selectors.
/// </para>
/// </summary>
public sealed class ResolutionsMap
{
    private readonly Dictionary<(Type, Type), AdapterResolution> adaptersResolutions = [];
    private readonly Dictionary<(Type, Type), object> adaptersFunctions = [];
    private readonly Dictionary<(Type, Type), object> adaptersExpressions = [];

    /// <summary>
    /// Add/store a new adaptar.
    /// </summary>
    /// <typeparam name="TSource">The source type of the adapter.</typeparam>
    /// <typeparam name="TTarget">The target type of the adapter.</typeparam>
    /// <param name="adapter">The adapter function.</param>
    public void AddAdapter<TSource, TTarget>(Func<TSource, TTarget> adapter)
    {
        adaptersFunctions.Add((typeof(TSource), typeof(TTarget)), adapter);
    }

    /// <summary>
    /// Try to get an adapter from the map.
    /// </summary>
    /// <typeparam name="TSource">The source type of the adapter.</typeparam>
    /// <typeparam name="TTarget">The target type of the adapter.</typeparam>
    /// <returns>The adapter function if it exists, otherwise null.</returns>
    public Func<TSource, TTarget>? GetAdapter<TSource, TTarget>()
    {
        if (adaptersFunctions.TryGetValue((typeof(TSource), typeof(TTarget)), out var obj)
            && obj is Func<TSource, TTarget> adapter)
        {
            return adapter;
        }

        return null;
    }

    /// <summary>
    /// Add/store a new adapter expression.
    /// </summary>
    /// <typeparam name="TSource">The source type of the adapter.</typeparam>
    /// <typeparam name="TTarget">The target type of the adapter.</typeparam>
    /// <param name="adapter">The adapter expression.</param>
    public void AddAdapterExpression<TSource, TTarget>(Expression<Func<TSource, TTarget>> adapter)
    {
        adaptersExpressions.Add((typeof(TSource), typeof(TTarget)), adapter);
    }

    /// <summary>
    /// Try to get an adapter expression from the map.
    /// </summary>
    /// <typeparam name="TSource">The source type of the adapter.</typeparam>
    /// <typeparam name="TTarget">The target type of the adapter.</typeparam>
    /// <returns>The adapter expression if it exists, otherwise null.</returns>
    public Expression<Func<TSource, TTarget>>? GetAdapterExpression<TSource, TTarget>()
    {
        if (adaptersExpressions.TryGetValue((typeof(TSource), typeof(TTarget)), out var obj)
            && obj is Expression<Func<TSource, TTarget>> adapter)
        {
            return adapter;
        }

        return null;
    }

    /// <summary>
    /// Add/store a new adapter resolution.
    /// </summary>
    /// <typeparam name="TSource">The source type of the adapter.</typeparam>
    /// <typeparam name="TTarget">The target type of the adapter.</typeparam>
    /// <param name="resolution">The adapter resolution.</param>
    public void AddAdapterResolution<TSource, TTarget>(AdapterResolution resolution)
    {
        adaptersResolutions.Add((typeof(TSource), typeof(TTarget)), resolution);
    }

    /// <summary>
    /// Try to get an adapter resolution from the map.
    /// </summary>
    /// <typeparam name="TSource">The source type of the adapter.</typeparam>
    /// <typeparam name="TTarget">The target type of the adapter.</typeparam>
    /// <returns>The adapter resolution if it exists, otherwise null.</returns>
    public AdapterResolution? GetAdapterResolution<TSource, TTarget>()
    {
        if (adaptersResolutions.TryGetValue((typeof(TSource), typeof(TTarget)), out var resolution))
        {
            return resolution;
        }

        return null;
    }
}
