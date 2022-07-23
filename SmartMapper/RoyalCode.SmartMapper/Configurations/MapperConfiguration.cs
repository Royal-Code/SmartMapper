using System.Linq.Expressions;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Configurations;

public class MapperConfiguration
{
    private readonly Resolutions resolutions = new();
    private readonly Dictionary<MapKey, IMapOptions> adaptersOptions = new();
    private readonly Dictionary<MapKey, IMapOptions> selectorsOptions = new();
    
    private readonly Dictionary<MapKey, MapOptions> mapOptions = new();

    public IMapOptions GetAdapterMapOptions(MapKey key)
    {
        if (adaptersOptions.TryGetValue(key, out var options))
            return options;

        var newOptions = new MapOptions();
        adaptersOptions.Add(key, newOptions);
        return newOptions;
    }
    
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

    // public IMapOptions Configure<TSource, TTarget>()
    // {
    //     var key = new MapKey(typeof(TSource), typeof(TTarget));
    //     if (mapOptions.TryGetValue(key, out var options))
    //         return options;
    //
    //     var newOptions = new MapOptions<TSource, TTarget>();
    //     mapOptions.Add(key, newOptions);
    //     return newOptions;
    // }
}