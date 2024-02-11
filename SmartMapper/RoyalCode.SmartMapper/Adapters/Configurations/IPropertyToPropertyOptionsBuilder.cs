
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Adapters.Configurations;

/// <summary>
/// <para>
///     A builder to configure the mapping of a source property to a target property.
/// </para>
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
/// <typeparam name="TTarget">The destination type.</typeparam>
/// <typeparam name="TSourceProperty">The source property type.</typeparam>
/// <typeparam name="TTargetProperty">The destination property type.</typeparam>
public interface IPropertyToPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty>
    : IPropertyStrategyBuilder<TSourceProperty, TTargetProperty, IPropertyToPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty>>
{
    /// <summary>
    /// <para>
    ///     Continues the mapping of the source property to an internal property of the target property.
    /// </para>
    /// </summary>
    /// <param name="propertySelector">
    ///     The property selection expression.
    /// </param>
    /// <typeparam name="TNextProperty">
    ///     The internal property type.
    /// </typeparam>
    /// <returns>
    ///     The builder to configure the property to property mapping.
    /// </returns>
    IPropertyThenOptionsBuilder<TSourceProperty, TTargetProperty, TNextProperty> ThenTo<TNextProperty>(
        Expression<Func<TTargetProperty, TNextProperty>> propertySelector);

    /// <summary>
    /// <para>
    ///     Continues the mapping of the source property to an internal property of the target property.
    /// </para>
    /// </summary>
    /// <returns>
    ///     The builder to configure the property to property mapping.
    /// </returns>
    IPropertyThenOptionsBuilder<TSourceProperty, TTargetProperty> Then();
}
