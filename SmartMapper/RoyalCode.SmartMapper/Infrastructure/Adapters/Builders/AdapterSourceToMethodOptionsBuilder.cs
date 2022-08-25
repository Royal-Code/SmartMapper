using System.Linq.Expressions;
using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Extensions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

/// <inheritdoc />
public class AdapterSourceToMethodOptionsBuilder<TSource, TTarget> 
    : IAdapterSourceToMethodOptionsBuilder<TSource, TTarget>
{
    private readonly AdapterOptions adapterOptions;
    private readonly SourceToMethodOptions methodOptions;

    public AdapterSourceToMethodOptionsBuilder(AdapterOptions adapterOptions, SourceToMethodOptions methodOptions)
    {
        this.adapterOptions = adapterOptions;
        this.methodOptions = methodOptions;
    }

    /// <inheritdoc />
    public void Parameters(Action<IAdapterSourceToMethodParametersOptionsBuilder<TSource>> configureParameters)
    {
        methodOptions.Strategy = SourceToMethodStrategy.SelectedParameters;
        
        var builder = new AdapterSourceToMethodParametersOptionsBuilder<TSource>(adapterOptions, methodOptions);
        configureParameters(builder);
    }

    /// <inheritdoc />
    public void AllProperties(Action<IAdapterSourceToMethodPropertiesOptionsBuilder<TSource>> configureProperties)
    {
        methodOptions.Strategy = SourceToMethodStrategy.AllParameters;
        
        var builder = new AdapterSourceToMethodPropertiesOptionsBuilder<TSource>(adapterOptions, methodOptions);
        configureProperties(builder);
    }

    /// <inheritdoc />
    public IAdapterSourceToMethodOptionsBuilder<TSource, TTarget> UseMethod(string name)
    {
        methodOptions.MethodOptions.WithMethodName(name);
        return this;
    }

    /// <inheritdoc />
    public IAdapterSourceToMethodOptionsBuilder<TSource, TTarget> UseMethod(Expression<Func<TTarget, Delegate>> methodSelector)
    {
        if (!methodSelector.TryGetMethod(out var method))
            throw new InvalidMethodDelegateException(nameof(methodSelector));
        
        methodOptions.MethodOptions.Method = method;
        
        return this;
    }
}