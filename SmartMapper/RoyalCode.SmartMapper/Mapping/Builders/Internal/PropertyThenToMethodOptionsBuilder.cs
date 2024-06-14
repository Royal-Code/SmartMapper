using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;
using RoyalCode.SmartMapper.Mapping.Options;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Mapping.Builders.Internal;

internal class PropertyThenToMethodOptionsBuilder<TTargetProperty, TSourceProperty>
    : IPropertyToMethodBuilder<TTargetProperty, TSourceProperty>
{
    private readonly ThenToMethodOptions options;

    /// <summary>
    /// Creates a new Options builder.
    /// </summary>
    /// <param name="options"></param>
    public PropertyThenToMethodOptionsBuilder(ThenToMethodOptions options)
    {
        this.options = options;
    }

    /// <inheritdoc />
    public void Parameters(Action<IPropertyToParametersBuilder<TSourceProperty>> configureParameters)
    {
        var innerPropertyOptions = options.MapInnerParameters();
        var builder = new PropertyToParametersBuilder<TSourceProperty>(
            innerPropertyOptions.InnerSourceOptions, options.MethodOptions);
        configureParameters(builder);
    }

    /// <inheritdoc />
    public IParameterBuilder<TSourceProperty> ToParameter(string? parameterName = null)
    {
        var resolutionOptionsBase = options.SourcePropertyOptions.ResolutionOptions
            ?? throw new InvalidOperationException("The source property must have a resolution");

        var parameterOptions = options.MapAsParameter();

        if (parameterName is not null)
            parameterOptions.UseParameterName(parameterName);

        return new ParameterBuilder<TSourceProperty>(resolutionOptionsBase);
    }

    /// <inheritdoc />
    public IPropertyToMethodBuilder<TTargetProperty, TSourceProperty> UseMethod(string name)
    {
        options.MethodOptions.WithMethodName(name);
        return this;
    }

    /// <inheritdoc />
    public IPropertyToMethodBuilder<TTargetProperty, TSourceProperty> UseMethod(
        Expression<Func<TTargetProperty, Delegate>> methodSelector)
    {
        if (!methodSelector.TryGetMethod(out var method))
            throw new InvalidMethodDelegateException(nameof(methodSelector));

        options.MethodOptions.Method = method;
        options.MethodOptions.MethodName = method.Name;
        return this;
    }
}
