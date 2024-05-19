using RoyalCode.SmartMapper.Adapters.Options;
using System.Linq.Expressions;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;
using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

/// <inheritdoc />
internal sealed class AdapterOptionsBuilder<TSource, TTarget> : IAdapterOptionsBuilder<TSource, TTarget>
{
    private readonly AdapterOptions options;

    /// <summary>
    /// Creates a new instance of <see cref="AdapterOptionsBuilder{TSource, TTarget}"/>.
    /// </summary>
    /// <param name="options"></param>
    public AdapterOptionsBuilder(AdapterOptions options)
    {
        this.options = options;
    }

    /// <inheritdoc />
    public IConstructorOptionsBuilder<TSource> Constructor()
    {
        return new ConstructorOptionsBuilder<TSource>(options);
    }

    /// <inheritdoc />
    public ISourceToMethodOptionsBuilder<TSource, TTarget> MapToMethod()
    {
        var methodOptions = options.TargetOptions.CreateMethodOptions();
        var sourceToMethodOptions = options.SourceOptions.CreateSourceToMethodOptions(methodOptions);
        
        var builder = new SourceToMethodOptionsBuilder<TSource, TTarget>(options, sourceToMethodOptions);
        return builder;
    }

    /// <inheritdoc />
    public ISourceToMethodOptionsBuilder<TSource, TTarget> MapToMethod(Expression<Func<TTarget, Delegate>> methodSelector)
    {
        if (!methodSelector.TryGetMethod(out var method))
            throw new InvalidMethodDelegateException(nameof(methodSelector));

        if(!options.SourceOptions.TryGetSourceToMethodOption(method, out var sourceToMethodOptions))
        {
            var methodOptions = options.TargetOptions.GetMethodOptions(method);
            sourceToMethodOptions = options.SourceOptions.CreateSourceToMethodOptions(methodOptions);
        }
        
        var builder = new SourceToMethodOptionsBuilder<TSource, TTarget>(options, sourceToMethodOptions);
        return builder;
    }

    /// <inheritdoc />
    public ISourceToMethodOptionsBuilder<TSource, TTarget> MapToMethod(string methodName)
    {
        if(!options.SourceOptions.TryGetSourceToMethodOption(methodName, out var sourceToMethodOptions))
        {
            var methodOptions = options.TargetOptions.GetMethodOptions(methodName);
            sourceToMethodOptions = options.SourceOptions.CreateSourceToMethodOptions(methodOptions);
        }
        
        var builder = new SourceToMethodOptionsBuilder<TSource, TTarget>(options, sourceToMethodOptions);
        return builder;
    }

    /// <inheritdoc />
    public IPropertyOptionsBuilder<TSource, TTarget, TProperty> Map<TProperty>(
        Expression<Func<TSource, TProperty>> propertySelector)
    {
        var propertyOptions = options.SourceOptions.GetPropertyOptions(propertySelector);

        var builder = new PropertyOptionsBuilder<TSource, TTarget, TProperty>(options, propertyOptions);
        return builder;
    }

    /// <inheritdoc />
    public IPropertyOptionsBuilder<TSource, TTarget, TProperty> Map<TProperty>(string propertyName)
    {
        var propertyOptions = options.SourceOptions.GetPropertyOptions(propertyName, typeof(TProperty));
        return new PropertyOptionsBuilder<TSource, TTarget, TProperty>(options, propertyOptions);
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
