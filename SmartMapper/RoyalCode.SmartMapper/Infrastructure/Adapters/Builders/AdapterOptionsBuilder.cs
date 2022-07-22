using System.Linq.Expressions;
using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Extensions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

public class AdapterOptionsBuilder<TSource, TTarget> : IAdapterOptionsBuilder<TSource, TTarget>
{
    private readonly AdapterOptions options = new();
    
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
            throw new ArgumentException("Invalid method selector");
        
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
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(methodName));
        
        var toMethodOptions = new AdapterSourceToMethodOptions
        {
            MethodName = methodName
        };
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