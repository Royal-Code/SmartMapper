using System.Linq.Expressions;
using RoyalCode.SmartMapper.Configurations;

namespace RoyalCode.SmartMapper.Configuring.Adapters;

public class AdapterOptionsBuilder : IAdapterOptionsBuilder
{
    private readonly IMapOptions mapOptions;

    public AdapterOptionsBuilder(IMapOptions mapOptions)
    {
        this.mapOptions = mapOptions;
    }

    public IAdapterOptionsBuilder Configure<TSource, TTarget>(Action<IAdapterOptionsBuilder<TSource, TTarget>> configure)
    {
        var builder = Configure<TSource, TTarget>();
        configure(builder);
        return this;
    }

    public IAdapterOptionsBuilder<TSource, TTarget> Configure<TSource, TTarget>()
    {
        return new AdapterOptionsBuilder<TSource, TTarget>(mapOptions);
    }
}

public class AdapterOptionsBuilder<TSource, TTarget> : IAdapterOptionsBuilder<TSource, TTarget>
{
    private readonly IMapOptions mapOptions;

    public AdapterOptionsBuilder(IMapOptions mapOptions)
    {
        this.mapOptions = mapOptions;
    }

    public IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty> Map<TProperty>(
        Expression<Func<TSource, TProperty>> propertySelection)
    {
        throw new NotImplementedException();
    }

    public IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty> Map<TProperty>(string propertyName)
    {
        throw new NotImplementedException();
    }
}