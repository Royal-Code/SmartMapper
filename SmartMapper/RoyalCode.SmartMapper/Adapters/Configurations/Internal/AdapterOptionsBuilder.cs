using RoyalCode.SmartMapper.Adapters.Options;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

/// <inheritdoc />
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

    public ISourceToMethodOptionsBuilder<TSource, TTarget> MapToMethod()
    {
        var builder = new SourceToMethodOptionsBuilder<TSource, TTarget>(options);
        return builder;
    }

    public ISourceToMethodOptionsBuilder<TSource, TTarget> MapToMethod(Expression<Func<TTarget, Delegate>> methodSelector)
    {
        var builder = new SourceToMethodOptionsBuilder<TSource, TTarget>(options, methodSelector);
        return builder;
    }

    public ISourceToMethodOptionsBuilder<TSource, TTarget> MapToMethod(string methodName)
    {
        var builder = new SourceToMethodOptionsBuilder<TSource, TTarget>(options, methodName);
        return builder;
    }

    public IPropertyOptionsBuilder<TSource, TTarget, TProperty> Map<TProperty>(Expression<Func<TSource, TProperty>> propertySelector)
    {
        throw new NotImplementedException();
    }

    public IPropertyOptionsBuilder<TSource, TTarget, TProperty> Map<TProperty>(string propertyName)
    {
        throw new NotImplementedException();
    }
}
