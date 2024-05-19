
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Adapters.Configurations;

/// <summary>
/// <para>
///     A builder to configure the mapping of a source property.
/// </para>
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
/// <typeparam name="TTarget">The destination type.</typeparam>
/// <typeparam name="TSourceProperty">The source property type.</typeparam>
public interface IPropertyOptionsBuilder<TSource, TTarget, TSourceProperty>
{
    /// <summary>
    /// <para>
    ///     Map to another property.
    /// </para>
    /// </summary>
    /// <typeparam name="TTargetProperty">The destination property type.</typeparam>
    /// <param name="propertySelector">The property selection expression.</param>
    /// <returns>
    ///     The builder to configure the property to property mapping.
    /// </returns>
    IPropertyToPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty> To<TTargetProperty>(
        Expression<Func<TTarget, TTargetProperty>> propertySelector);

    /// <summary>
    /// <para>
    ///     Map to another property.
    /// </para>
    /// </summary>
    /// <typeparam name="TTargetProperty">The destination property type.</typeparam>
    /// <param name="propertyName">The property name.</param>
    /// <returns>
    ///     The builder to configure the property to property mapping.
    /// </returns>
    IPropertyToPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty> To<TTargetProperty>(string propertyName);

    /// <summary>
    /// <para>
    ///     Maps the internal properties of the source property type to the destination type.
    /// </para>
    /// </summary>
    /// <param name="innerPropertiesBuilder">A builder to configure the inner properties.</param>
    void InnerProperties(Action<IInnerPropertiesOptionsBuilder<TSource, TTarget, TSourceProperty>>? innerPropertiesBuilder = null);
    
    /// <summary>
    /// <para>
    ///     Maps the internal properties of the source property type to a constructor.
    /// </para>
    /// </summary>
    /// <returns>
    ///     The builder to configure the property to constructor mapping.
    /// </returns>
    [Obsolete("Use ToConstructor from the IAdapterOptionsBuilder instead.")]
    IPropertyToConstructorOptionsBuilder<TSourceProperty> ToConstructor();

    /// <summary>
    /// Maps the current property to a method, where the internal properties will be mapped to the method parameters.
    /// </summary>
    /// <returns>
    ///     The builder to configure the property to method mapping.
    /// </returns>
    IPropertyToMethodOptionsBuilder<TTarget, TSourceProperty> ToMethod();

    /// <summary>
    /// Maps the current property to a method, where the internal properties will be mapped to the method parameters.
    /// </summary>
    /// <param name="methodSelector">
    ///     A function to select the method of the destination type.
    /// </param>
    /// <returns>
    ///     The builder to configure the property to method mapping.
    /// </returns>
    IPropertyToMethodOptionsBuilder<TTarget, TSourceProperty> ToMethod(
        Expression<Func<TTarget, Delegate>> methodSelector);
}