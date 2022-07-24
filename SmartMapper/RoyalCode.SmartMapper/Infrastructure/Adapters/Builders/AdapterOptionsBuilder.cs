using System.Linq.Expressions;
using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Extensions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

/// <inheritdoc />
public class AdapterOptionsBuilder<TSource, TTarget> : IAdapterOptionsBuilder<TSource, TTarget>
{
    private readonly AdapterOptions options;
    
    public AdapterOptionsBuilder(AdapterOptions options)
    {
        this.options = options;
    }

    public AdapterOptionsBuilder()
    {
        options = new(typeof(TSource), typeof(TTarget));
    }

    /// <inheritdoc />
    public IAdapterSourceToMethodOptionsBuilder<TSource, TTarget> MapToMethod()
    {
        var toMethodOptions = new AdapterSourceToMethodOptions();
        options.AddToMethod(toMethodOptions);
        return new AdapterSourceToMethodOptionsBuilder<TSource, TTarget>(options, toMethodOptions);
    }

    /// <inheritdoc />
    public IAdapterSourceToMethodOptionsBuilder<TSource, TTarget> MapToMethod(Expression<Func<TTarget, Delegate>> methodSelector)
    {
        if (!methodSelector.TryGetMethod(out var method))
            throw new InvalidMethodDelegateException(nameof(methodSelector));
        
        var toMethodOptions = new AdapterSourceToMethodOptions
        {
            Method = method
        };
        options.AddToMethod(toMethodOptions);
        return new AdapterSourceToMethodOptionsBuilder<TSource, TTarget>(options, toMethodOptions);
    }

    /// <inheritdoc />
    public IAdapterSourceToMethodOptionsBuilder<TSource, TTarget> MapToMethod(string methodName)
    {
        if (string.IsNullOrWhiteSpace(methodName))
            throw new InvalidMethodNameException("Value cannot be null or whitespace.", nameof(methodName));

        var methods = typeof(TTarget).GetMethods().Where(m => m.Name == methodName).ToList();
        if (methods.Count is 0)
            throw new InvalidMethodNameException(
                $"Method '{methodName}' not found on type '{typeof(TTarget).Name}'.", nameof(methodName));

        var toMethodOptions = new AdapterSourceToMethodOptions()
        {
            MethodName = methodName
        };
        if (methods.Count is 1)
            toMethodOptions.Method = methods[0];
        
        options.AddToMethod(toMethodOptions);
        return new AdapterSourceToMethodOptionsBuilder<TSource, TTarget>(options, toMethodOptions);
    }

    /// <inheritdoc />
    public IAdapterConstructorOptionsBuilder<TSource, TTarget> Constructor()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty> Map<TProperty>(Expression<Func<TSource, TProperty>> propertySelection)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty> Map<TProperty>(string propertyName)
    {
        throw new NotImplementedException();
    }
}