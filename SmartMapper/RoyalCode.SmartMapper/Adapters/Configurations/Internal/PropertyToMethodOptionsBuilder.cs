using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

internal sealed class PropertyToMethodOptionsBuilder<TTarget, TSourceProperty>
    : IPropertyToMethodOptionsBuilder<TTarget, TSourceProperty>
{
    private readonly ToMethodOptions options;

    public PropertyToMethodOptionsBuilder(ToMethodOptions options)
    {
        this.options = options;
    }

    public void Parameters(Action<IPropertyToParametersOptionsBuilder<TSourceProperty>> configureParameters)
    {
        var innerParameters = options.MapInnerParameters();
        var builder = new PropertyToParametersOptionsBuilder<TSourceProperty>(innerParameters);
        configureParameters(builder);
    }

    public void Value(Action<IParameterStrategyBuilder<TSourceProperty>> configureProperty)
    {
        var parameterOptions = options.MapAsParameter();

        throw new NotImplementedException();
    }

    public IPropertyToMethodOptionsBuilder<TTarget, TSourceProperty> UseMethod(string name)
    {
        options.MethodOptions.WithMethodName(name);
        return this;
    }

    public IPropertyToMethodOptionsBuilder<TTarget, TSourceProperty> UseMethod(Expression<Func<TTarget, Delegate>> methodSelector)
    {
        if (!methodSelector.TryGetMethod(out var method))
            throw new InvalidMethodDelegateException(nameof(methodSelector));

        options.MethodOptions.Method = method;
        options.MethodOptions.MethodName = method.Name;
        return this;
    }
}