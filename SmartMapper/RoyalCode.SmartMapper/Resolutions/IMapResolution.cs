using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Resolutions;

public interface IMapResolution<TTarget>
{
    bool RequiresServiceProvider { get; }
    
    void Map(object source, TTarget target, IServiceProvider? provider = null);
}

public interface IMapResolution<TSource, TTarget> : IMapResolution<TTarget>
{
    void Map(TSource source, TTarget target, IServiceProvider? provider = null);
    
    Expression<Action<TSource, TTarget, IServiceProvider?>> MapExpression { get; }
    
    Action<TSource, TTarget, IServiceProvider?> MapAction { get; }
}