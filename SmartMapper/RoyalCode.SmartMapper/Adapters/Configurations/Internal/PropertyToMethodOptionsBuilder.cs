using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

internal sealed class PropertyToMethodOptionsBuilder<TTarget, TSourceProperty>
    : IPropertyToMethodOptionsBuilder<TTarget, TSourceProperty>
{
    
    
    public void Parameters(Action<IPropertyToParametersOptionsBuilder<TSourceProperty>> configureParameters)
    {
        throw new NotImplementedException();
    }

    public void Value(Action<IParameterStrategyBuilder<TSourceProperty>> configureProperty)
    {
        throw new NotImplementedException();
    }

    public IPropertyToMethodOptionsBuilder<TTarget, TSourceProperty> UseMethod(string name)
    {
        throw new NotImplementedException();
    }

    public IPropertyToMethodOptionsBuilder<TTarget, TSourceProperty> UseMethod(Expression<Func<TTarget, Delegate>> methodSelector)
    {
        throw new NotImplementedException();
    }
}