
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Configurations.Adapters;

/// <summary>
/// <para>
///     A builder to configure the mapping of a source property.
/// </para>
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
/// <typeparam name="TTarget">The destination type.</typeparam>
/// <typeparam name="TSourceProperty">The source property type.</typeparam>
public interface IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty>
{
    /// <summary>
    /// <para>
    ///     Map to another property.
    /// </para>
    /// </summary>
    /// <typeparam name="TTargetProperty">The destination property type.</typeparam>
    /// <param name="propertySelection">The property selection expression.</param>
    /// <returns>
    ///     The builder to configure the property to property mapping.
    /// </returns>
    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty> To<TTargetProperty>(
        Expression<Func<TTarget, TTargetProperty>> propertySelection);

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
    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty> To<TTargetProperty>(string propertyName);

    /// <summary>
    /// <para>
    ///     Maps the internal properties of the source property type to a constructor.
    /// </para>
    /// </summary>
    /// <returns>
    ///     The builder to configure the property to constructor mapping.
    /// </returns>
    IAdapterPropertyToConstructorOptionsBuilder<TSource, TSourceProperty> ToConstructor();

    /// <summary>
    /// Maps the current property to a method, where the internal properties will be mapped to the method parameters.
    /// </summary>
    /// <returns>
    ///     The builder to configure the property to method mapping.
    /// </returns>
    IAdapterPropertyToMethodOptionsBuilder<TSource, TTarget, TSourceProperty> ToMethod();
    
    /// <summary>
    /// Maps the current property to a method, where the internal properties will be mapped to the method parameters.
    /// </summary>
    /// <param name="methodSelect">
    ///     A function to select the method of the destination type.
    /// </param>
    /// <returns>
    ///     The builder to configure the property to method mapping.
    /// </returns>
    IAdapterPropertyToMethodOptionsBuilder<TSource, TTarget, TSourceProperty> ToMethod(
        Expression<Func<TTarget, Delegate>> methodSelect);
}

/// <summary>
/// <para>
///     A builder to configure the mapping of a source property to a target property.
/// </para>
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
/// <typeparam name="TTarget">The destination type.</typeparam>
/// <typeparam name="TSourceProperty">The source property type.</typeparam>
/// <typeparam name="TTargetProperty">The destination property type.</typeparam>
public interface IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty>
    : IAdapterPropertyStrategyBuilder<TSourceProperty, TTargetProperty, IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty>>
{
    /// <summary>
    /// <para>
    ///     Continues the mapping of the source property to an internal property of the target property.
    /// </para>
    /// </summary>
    /// <param name="propertySelection">
    ///     The property selection expression.
    /// </param>
    /// <typeparam name="TNextProperty">
    ///     The internal property type.
    /// </typeparam>
    /// <returns>
    ///     The builder to configure the property to property mapping.
    /// </returns>
    IAdapterPropertyThenOptionsBuilder<TSourceProperty, TTargetProperty, TNextProperty> ThenTo<TNextProperty>(
        Expression<Func<TTargetProperty, TNextProperty>> propertySelection);

    /// <summary>
    /// <para>
    ///     Continues the mapping of the source property to an internal property of the target property.
    /// </para>
    /// </summary>
    /// <returns>
    ///     The builder to configure the property to property mapping.
    /// </returns>
    IAdapterPropertyThenOptionsBuilder<TSourceProperty, TTargetProperty> Then();
}
