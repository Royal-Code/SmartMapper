using System.Linq.Expressions;
using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Extensions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

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
    public void Parameters(Action<IAdapterSourceToMethodParametersOptionsBuilder<TSource, TTarget>> configureParameters)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void AllProperties(Action<IAdapterSourceToMethodPropertiesOptionsBuilder<TSource, TTarget>> configureProperties)
    {
        methodOptions.ClearParameters();
        
        throw new NotImplementedException();
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