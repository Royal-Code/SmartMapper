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
        methodOptions.ClearParameters();
        methodOptions.SourceToMethodStrategy = SourceToMethodStrategy.SelectedParameters;
        
        var builder = new AdapterSourceToMethodParametersOptionsBuilder<TSource>(adapterOptions, methodOptions);
        configureParameters(builder);
    }

    /// <inheritdoc />
    public void AllProperties(Action<IAdapterSourceToMethodPropertiesOptionsBuilder<TSource>> configureProperties)
    {
        methodOptions.ClearParameters();
        methodOptions.SourceToMethodStrategy = SourceToMethodStrategy.AllParameters;
        
        var builder = new AdapterSourceToMethodPropertiesOptionsBuilder<TSource>(adapterOptions, methodOptions);
        configureProperties(builder);
    }

    /// <inheritdoc />
    public IAdapterSourceToMethodOptionsBuilder<TSource, TTarget> UseMethod(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidMethodNameException("Value cannot be null or whitespace.", nameof(name));

        var methods = typeof(TTarget).GetMethods().Where(m => m.Name == name).ToList();
        if (methods.Count is 0)
            throw new InvalidMethodNameException(
                $"Method '{name}' not found on type '{typeof(TTarget).Name}'.", nameof(name));

        methodOptions.MethodName = name;
        if (methods.Count is 1)
            methodOptions.Method = methods[0];
        
        return this;
    }

    /// <inheritdoc />
    public IAdapterSourceToMethodOptionsBuilder<TSource, TTarget> UseMethod(Expression<Func<TTarget, Delegate>> methodSelector)
    {
        if (!methodSelector.TryGetMethod(out var method))
            throw new InvalidMethodDelegateException(nameof(methodSelector));
        
        methodOptions.Method = method;
        
        return this;
    }
}