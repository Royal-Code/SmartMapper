using System.Linq.Expressions;
using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Extensions;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

/// <inheritdoc />
public class AdapterSourceToMethodOptionsBuilder<TSource, TTarget> 
    : IAdapterSourceToMethodOptionsBuilder<TSource, TTarget>
{
    private readonly AdapterOptions adapterOptions;
    private readonly AdapterSourceToMethodOptions methodOptions;

    public AdapterSourceToMethodOptionsBuilder(AdapterOptions adapterOptions, AdapterSourceToMethodOptions methodOptions)
    {
        this.adapterOptions = adapterOptions;
        this.methodOptions = methodOptions;
    }

    /// <inheritdoc />
    public void Parameters(Action<IAdapterSourceToMethodParametersOptionsBuilder<TSource>> configureParameters)
    {
        methodOptions.ClearParameters();
        methodOptions.ParametersStrategy = ParametersStrategy.SelectedParameters;
        
        var builder = new AdapterSourceToMethodParametersOptionsBuilder<TSource>(adapterOptions, methodOptions);
        configureParameters(builder);
    }

    /// <inheritdoc />
    public void AllProperties(Action<IAdapterSourceToMethodPropertiesOptionsBuilder<TSource>> configureProperties)
    {
        methodOptions.ClearParameters();
        methodOptions.ParametersStrategy = ParametersStrategy.AllParameters;
        
        var builder = new AdapterSourceToMethodPropertiesOptionsBuilder<TSource>(adapterOptions, methodOptions);
        configureProperties(builder);
    }

    /// <inheritdoc />
    public IAdapterSourceToMethodOptionsBuilder<TSource, TTarget> UseMethod(string name)
    {
        methodOptions.MethodName = name;
        return this;
    }

    /// <inheritdoc />
    public IAdapterSourceToMethodOptionsBuilder<TSource, TTarget> UseMethod(Expression<Func<TTarget, Delegate>> methodSelector)
    {
        if (!methodSelector.TryGetMethod(out var method))
            throw new ArgumentException("Invalid method selector");
        
        methodOptions.Method = method;
        return this;
    }
}