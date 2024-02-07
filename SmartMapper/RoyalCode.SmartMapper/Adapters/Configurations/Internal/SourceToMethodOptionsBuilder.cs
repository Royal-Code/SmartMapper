using RoyalCode.SmartMapper.Adapters.Options;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

internal sealed class SourceToMethodOptionsBuilder<TSource, TTarget> : ISourceToMethodOptionsBuilder<TSource, TTarget>
{
    private readonly AdapterOptions adapterOptions;
    private readonly SourceToMethodOptions methodOptions;

    public void AllProperties(Action<ISourceToMethodPropertiesOptionsBuilder<TSource>> configureProperties)
    {
        throw new NotImplementedException();
    }

    public void Parameters(Action<ISourceToMethodParametersOptionsBuilder<TSource>> configureParameters)
    {
        throw new NotImplementedException();
    }

    public ISourceToMethodOptionsBuilder<TSource, TTarget> UseMethod(string name)
    {
        throw new NotImplementedException();
    }

    public ISourceToMethodOptionsBuilder<TSource, TTarget> UseMethod(Expression<Func<TTarget, Delegate>> methodSelector)
    {
        throw new NotImplementedException();
    }
}
