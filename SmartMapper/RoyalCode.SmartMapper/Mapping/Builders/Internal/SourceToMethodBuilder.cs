using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;
using RoyalCode.SmartMapper.Mapping.Options;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Mapping.Builders.Internal;

/// <inheritdoc />
internal sealed class SourceToMethodBuilder<TSource, TTarget> : ISourceToMethodBuilder<TSource, TTarget>
{
    private readonly SourceOptions sourceOptions;
    private readonly SourceToMethodOptions sourceToMethodOptions;

    public SourceToMethodBuilder(SourceOptions sourceOptions, SourceToMethodOptions sourceToMethodOptions)
    {
        this.sourceOptions = sourceOptions;
        this.sourceToMethodOptions = sourceToMethodOptions;
    }

    /// <inheritdoc />
    public void AllProperties(Action<ISourceToMethodPropertiesBuilder<TSource>>? configureProperties = null)
    {
        sourceToMethodOptions.UseAllProperties(out var propertiesOptions);

        if (configureProperties is null)
            return;

        var builder = new SourceToMethodPropertiesBuilder<TSource>(
            sourceOptions,
            propertiesOptions);

        configureProperties(builder);
    }

    /// <inheritdoc />
    public void Parameters(Action<ISourceToMethodParametersBuilder<TSource>> configureParameters)
    {
        sourceToMethodOptions.UseSelectedParameters(out var parametersOptions);
        
        var builder = new SourceToMethodParametersBuilder<TSource>(
            sourceOptions, parametersOptions);

        configureParameters(builder);
    }

    /// <inheritdoc />
    public ISourceToMethodBuilder<TSource, TTarget> UseMethod(string name)
    {
        sourceToMethodOptions.MethodOptions.WithMethodName(name);
        return this;
    }

    /// <inheritdoc />
    public ISourceToMethodBuilder<TSource, TTarget> UseMethod(Expression<Func<TTarget, Delegate>> methodSelector)
    {
        if (!methodSelector.TryGetMethod(out var method))
            throw new InvalidMethodDelegateException(nameof(methodSelector));

        sourceToMethodOptions.MethodOptions.Method = method;
        sourceToMethodOptions.MethodOptions.MethodName = method.Name;
        return this;
    }
}
