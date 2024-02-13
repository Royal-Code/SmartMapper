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
        // get target property by name, including inherited type properties
        var propertyInfo = typeof(TTargetProperty).GetRuntimeProperty(propertyName);

        // check if property exists
        if (propertyInfo is null)
            throw new InvalidPropertyNameException(
                $"The type '{typeof(TTarget).Name}' does not have a property with name '{propertyName}'.",
                nameof(propertyName));

        // validate the property type
        if (propertyInfo.PropertyType != typeof(TTargetProperty))
            throw new InvalidPropertyTypeException(
                $"Property '{propertyName}' on type '{typeof(TTarget).Name}' " +
                $"is not of type '{typeof(TTargetProperty).Name}', " +
                $"but of type '{propertyInfo.PropertyType.Name}'.",
                nameof(propertyName));

        var resolution = new ToPropertyResolutionOptions(propertyOptions, propertyInfo);

        var builder = new PropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty>(
            adapterOptions,
            resolution);

        return builder;
    }

    /// <inheritdoc />
    public IPropertyToConstructorOptionsBuilder<TProperty> ToConstructor()
    {
        var resolutionOptions = new ToConstructorResolutionOptions(propertyOptions);
        var innerAdapterOptions = new AdapterOptions(resolutionOptions.InnerSourceOptions, adapterOptions.TargetOptions);
        return new PropertyToConstructorOptionsBuilder<TProperty>(resolutionOptions, innerAdapterOptions);
    }

    /// <inheritdoc />
    public IPropertyToMethodOptionsBuilder<TTarget, TProperty> ToMethod()
    {
        var methodOptions = adapterOptions.TargetOptions.CreateMethodOptions();


        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IPropertyToMethodOptionsBuilder<TTarget, TProperty> ToMethod(
        Expression<Func<TTarget, Delegate>> methodSelect)
    {
        throw new NotImplementedException();
    }
}