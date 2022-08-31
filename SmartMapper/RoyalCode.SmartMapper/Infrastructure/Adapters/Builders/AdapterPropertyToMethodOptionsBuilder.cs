using System.Linq.Expressions;
using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Extensions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

/// <inheritdoc />
public class AdapterPropertyToMethodOptionsBuilder<TTarget, TProperty>
    : IAdapterPropertyToMethodOptionsBuilder<TTarget, TProperty>
{
    private readonly ToMethodOptions options;

    public AdapterPropertyToMethodOptionsBuilder(ToMethodOptions options)
    {
        this.options = options;
    }

    /// <inheritdoc />
    public void Parameters(Action<IAdapterPropertyToParametersOptionsBuilder<TProperty>> configureParameters)
    {
        options.Strategy = ToParametersStrategy.InnerProperties;
        var builder = new AdapterPropertyToMethodParametersOptionsBuilder<TProperty>(
            options.SourceOptions,
            options.MethodOptions);
        
        configureParameters(builder);
    }

    /// <inheritdoc />
    public void Value(Action<IAdapterParameterStrategyBuilder<TProperty>> configureProperty)
    {
        var assignmentOptions = options.ResolvedProperty?.GetOrCreateAssignmentStrategyOptions<TProperty>()
            ?? throw new InvalidOperationException("Property related options are not set");
        
        options.Strategy = ToParametersStrategy.Value;
        var builder = new AdapterParameterStrategyBuilder<TProperty>(assignmentOptions);
        configureProperty(builder);
    }

    /// <inheritdoc />
    public IAdapterPropertyToMethodOptionsBuilder<TTarget, TProperty> UseMethod(string name)
    {
        options.MethodOptions.WithMethodName(name);
        return this;
    }

    /// <inheritdoc />
    public IAdapterPropertyToMethodOptionsBuilder<TTarget, TProperty> UseMethod(
        Expression<Func<TTarget, Delegate>> methodSelector)
    {
        if (!methodSelector.TryGetMethod(out var method))
            throw new InvalidMethodDelegateException(nameof(methodSelector));
        
        options.MethodOptions.Method = method;
        
        return this;
    }
}