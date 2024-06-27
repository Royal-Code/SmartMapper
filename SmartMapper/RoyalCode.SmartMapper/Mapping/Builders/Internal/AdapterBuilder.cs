﻿using System.Linq.Expressions;
using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;
using RoyalCode.SmartMapper.Mapping.Options;
using RoyalCode.SmartMapper.Mapping.Options.Resolutions;

namespace RoyalCode.SmartMapper.Mapping.Builders.Internal;

/// <inheritdoc />
internal sealed class AdapterBuilder<TSource, TTarget> : IAdapterBuilder<TSource, TTarget>
{
    private readonly MappingOptions options;

    /// <summary>
    /// Creates a new instance of <see cref="AdapterBuilder{TSource, TTarget}"/>.
    /// </summary>
    /// <param name="options"></param>
    public AdapterBuilder(MappingOptions options)
    {
        this.options = options;
    }

    /// <inheritdoc />
    public IConstructorBuilder<TSource> Constructor()
    {
        return new ConstructorBuilder<TSource>(options);
    }

    /// <inheritdoc />
    public ISourceToMethodBuilder<TSource, TTarget> MapToMethod()
    {
        var methodOptions = options.TargetOptions.CreateMethodOptions();
        var sourceToMethodOptions = options.SourceOptions.CreateSourceToMethodOptions(methodOptions);

        var builder = new SourceToMethodBuilder<TSource, TTarget>(options.SourceOptions, sourceToMethodOptions);
        return builder;
    }

    /// <inheritdoc />
    public ISourceToMethodBuilder<TSource, TTarget> MapToMethod(Expression<Func<TTarget, Delegate>> methodSelector)
    {
        if (!methodSelector.TryGetMethod(out var method))
            throw new InvalidMethodDelegateException(nameof(methodSelector));

        if (!options.SourceOptions.TryGetSourceToMethodOption(method, out var sourceToMethodOptions))
        {
            var methodOptions = options.TargetOptions.GetMethodOptions(method);
            sourceToMethodOptions = options.SourceOptions.CreateSourceToMethodOptions(methodOptions);
        }

        var builder = new SourceToMethodBuilder<TSource, TTarget>(options.SourceOptions, sourceToMethodOptions);
        return builder;
    }

    /// <inheritdoc />
    public ISourceToMethodBuilder<TSource, TTarget> MapToMethod(string methodName)
    {
        if (!options.SourceOptions.TryGetSourceToMethodOption(methodName, out var sourceToMethodOptions))
        {
            var methodOptions = options.TargetOptions.GetMethodOptions(methodName);
            sourceToMethodOptions = options.SourceOptions.CreateSourceToMethodOptions(methodOptions);
        }

        var builder = new SourceToMethodBuilder<TSource, TTarget>(options.SourceOptions, sourceToMethodOptions);
        return builder;
    }

    /// <inheritdoc />
    public IPropertyBuilder<TSource, TTarget, TProperty> Map<TProperty>(
        Expression<Func<TSource, TProperty>> propertySelector)
    {
        var propertyOptions = options.SourceOptions.GetPropertyOptions(propertySelector);

        var builder = new PropertyBuilder<TSource, TTarget, TProperty>(options.TargetOptions, propertyOptions);
        return builder;
    }

    /// <inheritdoc />
    public IPropertyBuilder<TSource, TTarget, TProperty> Map<TProperty>(string propertyName)
    {
        var propertyOptions = options.SourceOptions.GetPropertyOptions(propertyName, typeof(TProperty));
        return new PropertyBuilder<TSource, TTarget, TProperty>(options.TargetOptions, propertyOptions);
    }

    /// <inheritdoc />
    public void Ignore<TProperty>(Expression<Func<TSource, TProperty>> propertySelector)
    {
        var propertyOptions = options.SourceOptions.GetPropertyOptions(propertySelector);
        IgnoreResolutionOptions.Resolves(propertyOptions);
    }

    /// <inheritdoc />
    public void Ignore(string propertyName)
    {
        var propertyOptions = options.SourceOptions.GetPropertyOptions(propertyName);
        IgnoreResolutionOptions.Resolves(propertyOptions);
    }
}