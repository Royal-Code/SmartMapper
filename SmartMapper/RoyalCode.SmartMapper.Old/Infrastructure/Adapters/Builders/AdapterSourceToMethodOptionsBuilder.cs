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
    private readonly SourceToMethodOptions sourceToMethodOptions;

    /// <summary>
    /// Create a new instance of <see cref="AdapterSourceToMethodOptionsBuilder{TSource,TTarget}"/>.
    /// </summary>
    /// <param name="adapterOptions">The adapter options.</param>
    /// <param name="sourceToMethodOptions">The source to method options.</param>
    public AdapterSourceToMethodOptionsBuilder(AdapterOptions adapterOptions, SourceToMethodOptions sourceToMethodOptions)
    {
        this.adapterOptions = adapterOptions;
        this.sourceToMethodOptions = sourceToMethodOptions;
    }

    /// <inheritdoc />
    public void Parameters(Action<IAdapterSourceToMethodParametersOptionsBuilder<TSource>> configureParameters)
    {
        sourceToMethodOptions.Strategy = SourceToMethodStrategy.SelectedParameters;
        
        var builder = new AdapterSourceToMethodParametersOptionsBuilder<TSource>(adapterOptions, sourceToMethodOptions);
        configureParameters(builder);
    }

    /// <inheritdoc />
    public void AllProperties(Action<IAdapterSourceToMethodPropertiesOptionsBuilder<TSource>> configureProperties)
    {
        sourceToMethodOptions.Strategy = SourceToMethodStrategy.AllParameters;
        
        var builder = new AdapterSourceToMethodPropertiesOptionsBuilder<TSource>(adapterOptions, sourceToMethodOptions);
        configureProperties(builder);
    }

    /// <inheritdoc />
    public IAdapterSourceToMethodOptionsBuilder<TSource, TTarget> UseMethod(string name)
    {
        sourceToMethodOptions.MethodOptions.WithMethodName(name);
        return this;
    }

    /// <inheritdoc />
    public IAdapterSourceToMethodOptionsBuilder<TSource, TTarget> UseMethod(Expression<Func<TTarget, Delegate>> methodSelector)
    {
        if (!methodSelector.TryGetMethod(out var method))
            throw new InvalidMethodDelegateException(nameof(methodSelector));
        
        sourceToMethodOptions.MethodOptions.Method = method;
        
        return this;
    }
}