using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Mapping.Builders;

/// <summary>
/// <para>
///     A builder to continue the mapping of a source property to a target property.
/// </para>
/// </summary>
/// <typeparam name="TSourceProperty">The source property type.</typeparam>
/// <typeparam name="TTargetProperty">The destination property type.</typeparam>
public interface IPropertyThenBuilder<TSourceProperty, TTargetProperty>
{
    /// <summary>
    /// <para>
    ///     Map to another property.
    /// </para>
    /// </summary>
    /// <typeparam name="TNextProperty">The destination property type.</typeparam>
    /// <param name="propertySelector">The property selection expression.</param>
    /// <returns>
    ///     The builder to configure the property to property mapping.
    /// </returns>
    IPropertyThenToBuilder<TSourceProperty, TTargetProperty, TNextProperty> To<TNextProperty>(
        Expression<Func<TTargetProperty, TNextProperty>> propertySelector);

    /// <summary>
    /// <para>
    ///     Map to another property.
    /// </para>
    /// </summary>
    /// <typeparam name="TNextProperty">The destination property type.</typeparam>
    /// <param name="propertyName">The property name.</param>
    /// <returns>
    ///     The builder to configure the property to property mapping.
    /// </returns>
    IPropertyThenToBuilder<TSourceProperty, TTargetProperty, TNextProperty> To<TNextProperty>(string propertyName);

    /// <summary>
    /// Maps the current property to a method, where the internal properties will be mapped to the method parameters.
    /// </summary>
    /// <returns>
    ///     The builder to configure the property to method mapping.
    /// </returns>
    IPropertyToMethodBuilder<TTargetProperty, TSourceProperty> ToMethod();

    /// <summary>
    /// Maps the current property to a method, where the internal properties will be mapped to the method parameters.
    /// </summary>
    /// <param name="methodSelector">
    ///     A function to select the method of the destination type.
    /// </param>
    /// <returns>
    ///     The builder to configure the property to method mapping.
    /// </returns>
    IPropertyToMethodBuilder<TTargetProperty, TSourceProperty> ToMethod(
        Expression<Func<TTargetProperty, Delegate>> methodSelector);
}
