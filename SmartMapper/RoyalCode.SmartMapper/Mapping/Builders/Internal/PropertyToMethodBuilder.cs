using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;
using System.Linq.Expressions;
using RoyalCode.SmartMapper.Mapping.Options.Resolutions;

namespace RoyalCode.SmartMapper.Mapping.Builders.Internal;

/// <inheritdoc />
internal sealed class PropertyToMethodBuilder<TTarget, TSourceProperty>
    : IPropertyToMethodBuilder<TTarget, TSourceProperty>
{
    private readonly PropertyToMethodResolutionOptions options;

    /// <summary>
    /// Creates a new instance of <see cref="PropertyToMethodBuilder{TTarget, TSourceProperty}"/>.
    /// </summary>
    /// <param name="options">The resolution options for the property to method mapping.</param>
    public PropertyToMethodBuilder(PropertyToMethodResolutionOptions options)
    {
        this.options = options;
    }

    /// <inheritdoc />
    public void Parameters(Action<IPropertyToParametersBuilder<TSourceProperty>> configureParameters)
    {
        var innerParameters = options.MapInnerParameters();
        var builder = new PropertyToParametersBuilder<TSourceProperty>(
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

        return new ParameterBuilder<TSourceProperty>(options);
    }

    /// <inheritdoc />
    public IPropertyToMethodBuilder<TTarget, TSourceProperty> UseMethod(string name)
    {
        options.MethodOptions.WithMethodName(name);
        return this;
    }

    /// <inheritdoc />
    public IPropertyToMethodBuilder<TTarget, TSourceProperty> UseMethod(
        Expression<Func<TTarget, Delegate>> methodSelector)
    {
        if (!methodSelector.TryGetMethod(out var method))
            throw new InvalidMethodDelegateException(nameof(methodSelector));

        options.MethodOptions.Method = method;
        options.MethodOptions.MethodName = method.Name;
        return this;
    }
}