using RoyalCode.SmartMapper.Adapters.Options.Resolutions;
using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

internal sealed class PropertyToMethodOptionsBuilder<TTarget, TSourceProperty>
    : IPropertyToMethodOptionsBuilder<TTarget, TSourceProperty>
{
    private readonly PropertyToMethodResolutionOptions options;

    public PropertyToMethodOptionsBuilder(PropertyToMethodResolutionOptions options)
    {
        this.options = options;
    }

    public void Parameters(Action<IPropertyToParametersOptionsBuilder<TSourceProperty>> configureParameters)
    {
        options.MapInnerParameters();

        throw new NotImplementedException();
    }

    public void Value(Action<IParameterStrategyBuilder<TSourceProperty>> configureProperty)
    {
        options.MapAsParameter();

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