using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;
using System.Linq.Expressions;
using System.Reflection;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

internal sealed class PropertyThenOptionsBuilder<TSourceProperty, TTargetProperty>
    : IPropertyThenOptionsBuilder<TSourceProperty, TTargetProperty>
{
    private readonly ThenToPropertyOptions thenToPropertyOptions;

    /// <summary>
    /// Creates a new instance of <see cref="PropertyThenToOptionsBuilder{TProperty, TTargetProperty, TNextProperty}"/>.
    /// </summary>
    /// <param name="thenToPropertyOptions"></param>
    public PropertyThenOptionsBuilder(ThenToPropertyOptions thenToPropertyOptions)
    {
        this.thenToPropertyOptions = thenToPropertyOptions;
    }

    public IPropertyThenToOptionsBuilder<TSourceProperty, TTargetProperty, TNextProperty> To<TNextProperty>(
        Expression<Func<TTargetProperty, TNextProperty>> propertySelector)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        var nextThenTo = thenToPropertyOptions.ThenTo<TNextProperty>(propertyInfo);
        var builder = new PropertyThenToOptionsBuilder<TSourceProperty, TTargetProperty, TNextProperty>(nextThenTo);
        return builder;
    }

    public IPropertyThenToOptionsBuilder<TSourceProperty, TTargetProperty, TNextProperty> To<TNextProperty>(
        string propertyName)
    {
        // get target property by name, including inherited type properties
        var propertyInfo = typeof(TTargetProperty).GetRuntimeProperty(propertyName);

        // check if property exists
        if (propertyInfo is null)
            throw new InvalidPropertyNameException(
                $"The type '{typeof(TTargetProperty).Name}' does not have a property with name '{propertyName}'.",
                nameof(propertyName));

        // validate the property type
        if (propertyInfo.PropertyType != typeof(TNextProperty))
            throw new InvalidPropertyTypeException(
                $"Property '{propertyName}' on type '{typeof(TTargetProperty).Name}' " +
                $"is not of type '{typeof(TNextProperty).Name}', " +
                $"but of type '{propertyInfo.PropertyType.Name}'.",
                nameof(propertyName));

        var nextThenTo = thenToPropertyOptions.ThenTo<TNextProperty>(propertyInfo);
        var builder = new PropertyThenToOptionsBuilder<TSourceProperty, TTargetProperty, TNextProperty>(nextThenTo);
        return builder;
    }

    public IPropertyToMethodOptionsBuilder<TTargetProperty, TSourceProperty> ToMethod()
    {
        // here is required a ThenToMethodOptions that contains the MethodOptions for the target property
        // The ThenToPropertyOptions should be used to create a new ThenToMethodOptions
        
        var resolution = new PropertyToMethodResolutionOptions(
            thenToPropertyOptions.SourcePropertyOptions,
            thenToPropertyOptions.MethodOptions);
        
        throw new NotImplementedException();
    }

    public IPropertyToMethodOptionsBuilder<TTargetProperty, TSourceProperty> ToMethod(
        Expression<Func<TTargetProperty, Delegate>> methodSelect)
    {
        throw new NotImplementedException();
    }
}
