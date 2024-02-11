using System.Linq.Expressions;
using System.Reflection;
using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;
using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

/// <inheritdoc />
internal sealed class PropertyOptionsBuilder<TSource, TTarget, TProperty> : IPropertyOptionsBuilder<TSource, TTarget, TProperty>
{
    private readonly AdapterOptions adapterOptions;
    private readonly PropertyOptions propertyOptions;

    public PropertyOptionsBuilder(AdapterOptions adapterOptions, PropertyOptions propertyOptions)
    {
        this.adapterOptions = adapterOptions;
        this.propertyOptions = propertyOptions;
    }
    
    /// <inheritdoc />
    public IPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> To<TTargetProperty>(
        Expression<Func<TTarget, TTargetProperty>> propertySelector)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        var resolution = new ToPropertyResolutionOptions(propertyOptions, propertyInfo);

        var builder = new PropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty>(
            adapterOptions, 
            resolution);
        
        return builder;
    }

    /// <inheritdoc />
    public IPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> To<TTargetProperty>(
        string propertyName)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IPropertyToConstructorOptionsBuilder<TProperty> ToConstructor()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IPropertyToMethodOptionsBuilder<TTarget, TProperty> ToMethod()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IPropertyToMethodOptionsBuilder<TTarget, TProperty> ToMethod(
        Expression<Func<TTarget, Delegate>> methodSelect)
    {
        throw new NotImplementedException();
    }
}