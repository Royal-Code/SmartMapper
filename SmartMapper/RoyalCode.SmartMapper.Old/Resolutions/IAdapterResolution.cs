using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Resolutions;

public interface IAdapterResolution<TTarget>
{
    bool RequiresServiceProvider { get; }
    
    TTarget Adapt(object source, IServiceProvider? provider = null);
}

public interface IAdapterResolution<TSource, TTarget> : IAdapterResolution<TTarget>
{
    TTarget Adapt(TSource source, IServiceProvider? provider = null);
    
    Expression<Func<TSource, IServiceProvider?, TTarget>> AdaptExpression { get; }
    
    Func<TSource, IServiceProvider?, TTarget> AdaptFunction { get; }
}