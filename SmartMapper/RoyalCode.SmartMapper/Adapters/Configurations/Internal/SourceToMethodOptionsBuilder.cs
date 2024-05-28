using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

internal sealed class SourceToMethodOptionsBuilder<TSource, TTarget> : ISourceToMethodOptionsBuilder<TSource, TTarget>
{
    private readonly AdapterOptions adapterOptions;
    private readonly SourceToMethodOptions sourceToMethodOptions;

    public SourceToMethodOptionsBuilder(AdapterOptions adapterOptions, SourceToMethodOptions sourceToMethodOptions)
    {
        this.adapterOptions = adapterOptions;
        this.sourceToMethodOptions = sourceToMethodOptions;
    }

    public void AllProperties(Action<ISourceToMethodPropertiesOptionsBuilder<TSource>>? configureProperties = null)
    {
        sourceToMethodOptions.Strategy = SourceToMethodStrategy.AllParameters;

        if (configureProperties is null)
            return;

        var builder = new SourceToMethodPropertiesOptionsBuilder<TSource>(
            adapterOptions.SourceOptions, 
            sourceToMethodOptions);

        configureProperties(builder);
    }

    /// <inheritdoc />
    public void Parameters(Action<ISourceToMethodParametersOptionsBuilder<TSource>> configureParameters)
    {
        var builder = new SourceToMethodParametersOptionsBuilder<TSource>(
            adapterOptions.SourceOptions, 
            sourceToMethodOptions);
        configureParameters(builder);
    }

    public ISourceToMethodOptionsBuilder<TSource, TTarget> UseMethod(string name)
    {
        sourceToMethodOptions.MethodOptions.WithMethodName(name);
        return this;
    }

    public ISourceToMethodOptionsBuilder<TSource, TTarget> UseMethod(Expression<Func<TTarget, Delegate>> methodSelector)
    {
        if (!methodSelector.TryGetMethod(out var method))
            throw new InvalidMethodDelegateException(nameof(methodSelector));

        sourceToMethodOptions.MethodOptions.Method = method;
        sourceToMethodOptions.MethodOptions.MethodName = method.Name;
        return this;
    }
}
