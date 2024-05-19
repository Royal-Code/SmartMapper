﻿using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

internal class PropertyThenToMethodOptionsBuilder<TTargetProperty, TSourceProperty>
    : IPropertyToMethodOptionsBuilder<TTargetProperty, TSourceProperty>
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
    public void Parameters(Action<IPropertyToParametersOptionsBuilder<TSourceProperty>> configureParameters)
    {
        var innerPropertyOptions = options.MapInnerParameters();
        var builder = new PropertyToParametersOptionsBuilder<TSourceProperty>(
            innerPropertyOptions.InnerSourceOptions, options.MethodOptions);
        configureParameters(builder);
    }

    /// <inheritdoc />
    public void ToParameter(Action<IToParameterOptionsBuilder<TSourceProperty>>? configureProperty = null)
    {
        var resolutionOptionsBase = options.SourcePropertyOptions.ResolutionOptions
            ?? throw new InvalidOperationException("The source property must have a resolution");

        options.MapAsParameter();

        if (configureProperty is not null)
        {
            var builder = new ToParameterOptionsBuilder<TSourceProperty>(resolutionOptionsBase);
            configureProperty(builder);
        }
    }

    /// <inheritdoc />
    public IPropertyToMethodOptionsBuilder<TTargetProperty, TSourceProperty> UseMethod(string name)
    {
        options.MethodOptions.WithMethodName(name);
        return this;
    }

    /// <inheritdoc />
    public IPropertyToMethodOptionsBuilder<TTargetProperty, TSourceProperty> UseMethod(
        Expression<Func<TTargetProperty, Delegate>> methodSelector)
    {
        if (!methodSelector.TryGetMethod(out var method))
            throw new InvalidMethodDelegateException(nameof(methodSelector));

        options.MethodOptions.Method = method;
        options.MethodOptions.MethodName = method.Name;
        return this;
    }
}
