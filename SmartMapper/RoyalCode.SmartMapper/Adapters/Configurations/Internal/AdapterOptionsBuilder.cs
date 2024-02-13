using RoyalCode.SmartMapper.Adapters.Options;
using System.Linq.Expressions;
using System.Reflection;
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
        var builder = new SourceToMethodOptionsBuilder<TSource, TTarget>(options);
        return builder;
    }

    /// <inheritdoc />
    public ISourceToMethodOptionsBuilder<TSource, TTarget> MapToMethod(Expression<Func<TTarget, Delegate>> methodSelector)
    {
        var builder = new SourceToMethodOptionsBuilder<TSource, TTarget>(options, methodSelector);
        return builder;
    }

    /// <inheritdoc />
    public ISourceToMethodOptionsBuilder<TSource, TTarget> MapToMethod(string methodName)
    {
        var builder = new SourceToMethodOptionsBuilder<TSource, TTarget>(options, methodName);
        return builder;
    }

    /// <inheritdoc />
    public IPropertyOptionsBuilder<TSource, TTarget, TProperty> Map<TProperty>(Expression<Func<TSource, TProperty>> propertySelector)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        var propertyOptions = options.SourceOptions.GetPropertyOptions(propertyInfo);

        var builder = new PropertyOptionsBuilder<TSource, TTarget, TProperty>(options, propertyOptions);
        return builder;
    }

    /// <inheritdoc />
    public IPropertyOptionsBuilder<TSource, TTarget, TProperty> Map<TProperty>(string propertyName)
    {
        var propertyOptions = options.SourceOptions.GetPropertyOptions(propertyName);
        
        // validate the property type
        if (propertyOptions.Property.PropertyType != typeof(TProperty))
            throw new InvalidPropertyTypeException(
                $"Property '{propertyName}' on type '{typeof(TSource).Name}' " +
                $"is not of type '{typeof(TProperty).Name}', " +
                $"but of type '{propertyOptions.Property.PropertyType.Name}'.",
                nameof(propertyName));
            
        return new PropertyOptionsBuilder<TSource, TTarget, TProperty>(options, propertyOptions);
    }
}
