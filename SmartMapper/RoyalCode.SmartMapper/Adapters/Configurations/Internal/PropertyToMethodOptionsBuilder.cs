using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;
using System.Linq.Expressions;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;

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
        var builder = new PropertyToParametersOptionsBuilder<TSourceProperty>(innerParameters, options.MethodOptions);
        configureParameters(builder);
    }

    /// <inheritdoc />
    public void Value(Action<IToParameterOptionsBuilder<TSourceProperty>>? configureProperty = null)
    {
        options.MapAsParameter();
        if (configureProperty is not null)
        {
            var builder = new ToParameterOptionsBuilder<TSourceProperty>(options);
            configureProperty(builder);
        }
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