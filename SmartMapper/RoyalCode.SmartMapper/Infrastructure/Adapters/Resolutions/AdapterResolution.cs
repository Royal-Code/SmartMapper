using System.Linq.Expressions;
using FastExpressionCompiler;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Infrastructure.Core;
using RoyalCode.SmartMapper.Resolutions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;

public class AdapterResolution : ResolutionBase
{
    
}

public class AdapterResolution<TSource, TTarget> : IAdapterResolution<TSource, TTarget>
{
    private Func<TSource, IServiceProvider?, TTarget>? resolution;

    public bool RequiresServiceProvider { get; }

    public Expression<Func<TSource, IServiceProvider?, TTarget>> AdaptExpression { get; }

    public Func<TSource, IServiceProvider?, TTarget> AdaptFunction => resolution ??= AdaptExpression.CompileFast();

    public AdapterResolution(
        bool requiresServiceProvider,
        Expression<Func<TSource, IServiceProvider?, TTarget>> adaptExpression)
    {
        RequiresServiceProvider = requiresServiceProvider;
        AdaptExpression = adaptExpression;
    }

    public TTarget Adapt(object source, IServiceProvider? provider = null)
    {
        if (RequiresServiceProvider && provider == null)
            throw new RequiredServiceProviderException(nameof(provider), typeof(TSource), typeof(TTarget));

        if (source is TSource sourceObject)
            return AdaptFunction(sourceObject, provider);

        throw new NotExpectedSourceTypeException(nameof(source), typeof(TSource), source.GetType());
    }

    public TTarget Adapt(TSource source, IServiceProvider? provider = null)
    {
        if (RequiresServiceProvider && provider == null)
            throw new RequiredServiceProviderException(nameof(provider), typeof(TSource), typeof(TTarget));

        return AdaptFunction(source, provider);
    }
}