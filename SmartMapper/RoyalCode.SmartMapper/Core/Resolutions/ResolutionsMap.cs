
using System.Linq.Expressions;

namespace RoyalCore.SmartMapper.Core.Resolutions;

/// <summary>
/// <para>
///     A map of resolutions for the mappers, adapters, and selectors.
/// </para>
/// </summary>
public sealed class ResolutionsMap
{
    private readonly Dictionary<(Type, Type), object> adaptersFunctions = [];
    private readonly Dictionary<(Type, Type), object> adaptersExpressions = [];

    public void AddAdapter<TSource, TTarget>(Func<TSource, TTarget> adapter)
    {
        adaptersFunctions.Add((typeof(TSource), typeof(TTarget)), adapter);
    }

    public Func<TSource, TTarget>? GetAdapter<TSource, TTarget>()
    {
        if (adaptersFunctions.TryGetValue((typeof(TSource), typeof(TTarget)), out var obj)
            && obj is Func<TSource, TTarget> adapter)
        {
            return adapter;
        }

        return null;
    }

    public void AddAdapterExpression<TSource, TTarget>(Expression<Func<TSource, TTarget>> adapter)
    {
        adaptersExpressions.Add((typeof(TSource), typeof(TTarget)), adapter);
    }

    public Expression<Func<TSource, TTarget>>? GetAdapterExpression<TSource, TTarget>()
    {
        if (adaptersExpressions.TryGetValue((typeof(TSource), typeof(TTarget)), out var obj)
            && obj is Expression<Func<TSource, TTarget>> adapter)
        {
            return adapter;
        }

        return null;
    }
}
