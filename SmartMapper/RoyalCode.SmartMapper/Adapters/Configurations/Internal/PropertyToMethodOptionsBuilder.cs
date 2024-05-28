using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;
using System.Linq.Expressions;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;
using RoyalCode.SmartMapper.Mapping.Builders;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

/// <inheritdoc />
internal sealed class PropertyToMethodOptionsBuilder<TTarget, TSourceProperty>
    : IPropertyToMethodOptionsBuilder<TTarget, TSourceProperty>
{
    private readonly PropertyToMethodResolutionOptions options;

    /// <summary>
    /// Creates a new instance of <see cref="PropertyToMethodOptionsBuilder{TTarget, TSourceProperty}"/>.
    /// </summary>
    /// <param name="options">The resolution options for the property to method mapping.</param>
    public PropertyToMethodOptionsBuilder(PropertyToMethodResolutionOptions options)
    {
        this.options = options;
    }

    /// <inheritdoc />
    public void Parameters(Action<IPropertyToParametersOptionsBuilder<TSourceProperty>> configureParameters)
    {
        var innerParameters = options.MapInnerParameters();
        var builder = new PropertyToParametersOptionsBuilder<TSourceProperty>(
            innerParameters.InnerSourceOptions, 
            options.MethodOptions);
        configureParameters(builder);
    }
    
    /// <inheritdoc />
    public IParameterBuilder<TSourceProperty> ToParameter(string? parameterName = null)
    {
        var parameterOptions = options.MapAsParameter();
        
        if (parameterName is not null)
            parameterOptions.UseParameterName(parameterName);
        
        return new ToParameterOptionsBuilder<TSourceProperty>(options);
    }

    /// <inheritdoc />
    public IPropertyToMethodOptionsBuilder<TTarget, TSourceProperty> UseMethod(string name)
    {
        options.MethodOptions.WithMethodName(name);
        return this;
    }

    /// <inheritdoc />
    public IPropertyToMethodOptionsBuilder<TTarget, TSourceProperty> UseMethod(
        Expression<Func<TTarget, Delegate>> methodSelector)
    {
        if (!methodSelector.TryGetMethod(out var method))
            throw new InvalidMethodDelegateException(nameof(methodSelector));

        options.MethodOptions.Method = method;
        options.MethodOptions.MethodName = method.Name;
        return this;
    }
}