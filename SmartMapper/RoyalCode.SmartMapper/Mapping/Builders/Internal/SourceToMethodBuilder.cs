using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;
using RoyalCode.SmartMapper.Mapping.Options;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Mapping.Builders.Internal;

/// <inheritdoc />
internal sealed class SourceToMethodBuilder<TSource, TTarget> : ISourceToMethodBuilder<TSource, TTarget>
{
    private readonly MappingOptions mappingOptions;
    private readonly SourceToMethodOptions sourceToMethodOptions;

    public SourceToMethodBuilder(MappingOptions mappingOptions, SourceToMethodOptions sourceToMethodOptions)
    {
        this.mappingOptions = mappingOptions;
        this.sourceToMethodOptions = sourceToMethodOptions;
    }

    /// <inheritdoc />
    public void AllProperties(Action<ISourceToMethodPropertiesBuilder<TSource>>? configureProperties = null)
    {
        sourceToMethodOptions.Strategy = SourceToMethodStrategy.AllParameters;

        if (configureProperties is null)
            return;

        var builder = new SourceToMethodPropertiesBuilder<TSource>(
            mappingOptions.SourceOptions,
            sourceToMethodOptions);

        configureProperties(builder);
    }

    /// <inheritdoc />
    public void Parameters(Action<ISourceToMethodParametersBuilder<TSource>> configureParameters)
    {
        sourceToMethodOptions.Strategy = SourceToMethodStrategy.SelectedParameters;

        var builder = new SourceToMethodParametersBuilder<TSource>(
            mappingOptions.SourceOptions,
            sourceToMethodOptions);

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
