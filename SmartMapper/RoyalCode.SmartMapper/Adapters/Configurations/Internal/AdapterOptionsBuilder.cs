using RoyalCode.SmartMapper.Adapters.Options;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

internal sealed class AdapterOptionsBuilder<TSource, TTarget> : IAdapterOptionsBuilder<TSource, TTarget>
{
    private readonly AdapterOptions options;

    public AdapterOptionsBuilder(AdapterOptions options)
    {
        this.options = options;
    }

    public IConstructorOptionsBuilder<TSource> Constructor()
    {
        return new ConstructorOptionsBuilder<TSource>(options);
    }

    public IAdapterSourceToMethodOptionsBuilder<TSource, TTarget> MapToMethod()
    {
        throw new NotImplementedException();
    }

    public IAdapterSourceToMethodOptionsBuilder<TSource, TTarget> MapToMethod(Expression<Func<TTarget, Delegate>> methodSelector)
    {
        throw new NotImplementedException();
    }

    public IAdapterSourceToMethodOptionsBuilder<TSource, TTarget> MapToMethod(string methodName)
    {
        throw new NotImplementedException();
    }

    public IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty> Map<TProperty>(Expression<Func<TSource, TProperty>> propertySelector)
    {
        throw new NotImplementedException();
    }

    public IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty> Map<TProperty>(string propertyName)
    {
        throw new NotImplementedException();
    }
}
