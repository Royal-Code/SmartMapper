using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Configurations;

public class MapperConfiguration
{
    private readonly Resolutions resolutions = new();
    private readonly Dictionary<MapKey, MapOptions> mapOptions = new();

    public Expression<Func<TFrom, TTo>> GetAdapterExpression<TFrom, TTo>()
    {
        var expr = resolutions.TryGetAdapterExpression<TFrom, TTo>();
        if (expr is null)
        {
            expr = CreateAdapterExpression<TFrom, TTo>();
            expr = resolutions.TryAddAdapterExpression(expr);
        }
        return expr;
    }

    private Expression<Func<TFrom, TTo>> CreateAdapterExpression<TFrom, TTo>()
    {

        throw new NotImplementedException();
    }

    public MapOptions<TSource, TTarget> Configure<TSource, TTarget>()
    {
        var key = new MapKey(typeof(TSource), typeof(TTarget));
        if (mapOptions.TryGetValue(key, out var options))
            return (MapOptions<TSource, TTarget>) options;

        var newOptions = new MapOptions<TSource, TTarget>();
        mapOptions.Add(key, newOptions);
        return newOptions;
    }
}