using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Configurations;

/// <summary>
/// <para>
///     A class to store all resolutions of type mappings.
/// </para>
/// </summary>
[Obsolete]
public class Resolutions
{
    private readonly ConcurrentDictionary<Tuple<Type, Type>, object> adapterExpressions = new();
    private readonly ConcurrentDictionary<Tuple<Type, Type>, object> resolutions = new();

    private Resolution<TFrom, TTo> GetResolution<TFrom, TTo>()
    {
        var key = new Tuple<Type, Type>(typeof(TFrom), typeof(TTo));
        var map = resolutions.GetOrAdd(key, _ => new Resolution<TFrom, TTo>());
        return (Resolution<TFrom, TTo>)map;
    }

    public Expression<Func<TFrom, TTo>>? TryGetAdapterExpression<TFrom, TTo>()
    {
        var resolution = GetResolution<TFrom, TTo>();
        return resolution.TryGetAdapterExpression();
    }

    public Expression<Func<TFrom, TTo>> TryAddAdapterExpression<TFrom, TTo>(Expression<Func<TFrom, TTo>> expr)
    {
        var resolution = GetResolution<TFrom, TTo>();
        resolution.AddAdapterExpression(expr);
        return expr;
    }
}