using System.Linq.Expressions;
using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Extensions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

/// <inheritdoc />
public class AdapterPropertyToMethodOptionsBuilder<TTarget, TProperty>
    : IAdapterPropertyToMethodOptionsBuilder<TTarget, TProperty>
{
    private readonly PropertyToMethodOptions options;

    public AdapterPropertyToMethodOptionsBuilder(PropertyToMethodOptions options)
    {
        this.options = options;
    }

    /// <inheritdoc />
    public void Parameters(Action<IAdapterPropertyToParametersOptionsBuilder<TProperty>> configureParameters)
    {
        options.Strategy = ToParametersStrategy.InnerProperties;
        configureParameters.Invoke(new AdapterPropertyToParametersOptionsBuilder<TProperty>(options));
    }

    /// <inheritdoc />
    public void Value(Action<IAdapterParameterStrategyBuilder<TProperty>> configureProperty)
    {
        var assignmentOptions = options.PropertyRelated?.GetOrCreateAssignmentStrategyOptions<TProperty>()
            ?? throw new InvalidOperationException("Property related options are not set");
        
        options.Strategy = ToParametersStrategy.Value;
        configureProperty.Invoke(new AdapterParameterStrategyBuilder<TProperty>(assignmentOptions));
    }

    /// <inheritdoc />
    public IAdapterPropertyToMethodOptionsBuilder<TTarget, TProperty> UseMethod(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidMethodNameException("Value cannot be null or whitespace.", nameof(name));

        var methods = typeof(TTarget).GetMethods().Where(m => m.Name == name).ToList();
        if (methods.Count is 0)
            throw new InvalidMethodNameException(
                $"Method '{name}' not found on type '{typeof(TTarget).Name}'.", nameof(name));

        options.MethodName = name;
        if (methods.Count is 1)
            options.Method = methods[0];

        return this;
    }

    /// <inheritdoc />
    public IAdapterPropertyToMethodOptionsBuilder<TTarget, TProperty> UseMethod(
        Expression<Func<TTarget, Delegate>> methodSelector)
    {
        if (!methodSelector.TryGetMethod(out var method))
            throw new InvalidMethodDelegateException(nameof(methodSelector));
        
        options.Method = method;
        
        return this;
    }
}